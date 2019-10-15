using ContestantRegister.Framework.Cqrs;
using OfficeOpenXml;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Individual.Queries
{
    public class ExportIndividualContestParticipantsResult
    {
        public ExcelPackage ExcelPackage { get; set; }

        public string ContestName { get; set; }
    }
    public class GetExportedIndividualContestParticipantsQuery : IQuery<ExportIndividualContestParticipantsResult>
    {
        public int ContestId { get; set; }
    }
}
