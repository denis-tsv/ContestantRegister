﻿using System;
using System.Collections.Generic;
using System.Text;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels.SelectedListItem;
using ContestantRegister.Framework.Cqrs;
using ContestantRegister.Models;

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
