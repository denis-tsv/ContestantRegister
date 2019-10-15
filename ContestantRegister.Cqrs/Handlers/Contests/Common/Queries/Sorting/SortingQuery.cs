using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels;
using ContestantRegister.Framework.Cqrs;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Queries
{

    public class SortingQueryResult
    {
        public int[] CompClassIds { get; set; }
        public SortingViewModel ViewModel { get; set; }
    }

    public class SortingQuery : IQuery<SortingQueryResult>
    {
        public int ContestId { get; set; }
    }
}
