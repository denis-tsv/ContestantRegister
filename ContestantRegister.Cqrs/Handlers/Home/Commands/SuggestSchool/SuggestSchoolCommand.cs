using ContestantRegister.Cqrs.Features.Frontend.Home.ViewModels;
using ContestantRegister.Framework.Cqrs;

namespace ContestantRegister.Application.Handlers.Frontend.Handlers.Home.Commands
{
    public class SuggestSchoolCommand : ICommand
    {
        public SuggectSchoolViewModel ViewModel { get; set; }
    }
}
