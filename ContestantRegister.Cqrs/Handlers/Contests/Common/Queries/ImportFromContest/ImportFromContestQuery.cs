using System.Collections.Generic;
using ContestantRegister.Framework.Cqrs;
using ContestantRegister.Models;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Queries
{
    public class ImportFromContestQuery : IQuery<List<Contest>>
    {
        public int ContestId { get; set; }
    }
}
