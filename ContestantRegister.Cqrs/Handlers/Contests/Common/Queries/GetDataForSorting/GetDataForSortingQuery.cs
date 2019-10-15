using System.Collections.Generic;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels.SelectedListItem;
using ContestantRegister.Framework.Cqrs;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Queries
{
    public class DataForSorting
    {
        public List<CompClassSelectedListItemViewModel> CompClasses { get; set; }
        public List<ContestAreaSelectedListItemViewModel> ContestAreas { get; set; }
    }

    public class GetDataForSortingQuery : IQuery<DataForSorting>
    {
        public int ContestId { get; set; }
        public int? SelectedContestAreaId { get; set; }
        public int[] SelectedCompClassIds { get; set; }
    }
}
