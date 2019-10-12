using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace ReferencesAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ReferencesAnalyzerAnalyzer : DiagnosticAnalyzer
    {
        private List<string> _contexts = new List<string>
        {
            "Admin", "Frontend"
        };

        private List<(string current, string notAllowed)> _layers = new List<(string, string)>
        {
            ( "UseCases", "DataAccess"),
            ( "UseCases", "Infrastructure.Implementation"),     
            //Если выделить контроллеры в отельный слой контроллеры, то с них нужно запретить ссылки на DomainServices, DataAccess, Infrastructure (и интерфейсы, и реализация)
        };

        private const string Category = "InvalidReference";

        private static DiagnosticDescriptor CrossContextReference = new DiagnosticDescriptor("CrosCtx", "Cross-context reference", "Cross-context reference", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: "Cross-context reference");
        private static DiagnosticDescriptor CrossLevelReference = new DiagnosticDescriptor("CrosLvl", "Cross-level reference", "Cross-level reference", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: "Cross-level reference");

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(CrossContextReference, CrossLevelReference); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.UsingDirective);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var namespaceName = ((UsingDirectiveSyntax)context.Node).Name.ToString();
            var assemblyName = context.Compilation.AssemblyName;

            for (int i = 0; i < _contexts.Count; i++)
            {
                for (int j = 0; j < _contexts.Count; j++)
                {
                    if (i == j) continue;

                    if (namespaceName.Contains(_contexts[i]) && assemblyName.Contains(_contexts[j]))
                    {
                        context.ReportDiagnostic(Diagnostic.Create(CrossContextReference, context.Node.GetLocation()));
                    }
                }
            }

            foreach (var layer in _layers)
            {
                if (assemblyName.Contains(layer.current) && namespaceName.Contains(layer.notAllowed))
                {
                    context.ReportDiagnostic(Diagnostic.Create(CrossLevelReference, context.Node.GetLocation()));
                }
            }
        }
    }
}
