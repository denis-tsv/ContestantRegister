using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContestantRegister.Cqrs.Features._Common.QueryHandlers;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Queries;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.Exceptions;
using ContestantRegister.Services.Extensions;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.QueryHandlers
{
    internal class ImportFromContestQueryHandler : ReadRepositoryQueryHandler<ImportFromContestQuery, List<Contest>>
    {
        public ImportFromContestQueryHandler(IReadRepository repository) : base(repository)
        {
        }

        public override async Task<List<Contest>> HandleAsync(ImportFromContestQuery query)
        {
            var contest = await ReadRepository.Set<Contest>().SingleOrDefaultAsync(c => c.Id == query.ContestId);
            if (contest == null) throw new EntityNotFoundException();
            
            if (string.IsNullOrEmpty(contest.YaContestAccountsCSV))
            {
                throw new InvalidOperationException("В контесте нет логинов-паролей для участников");
            }

            var contests = await ReadRepository.Set<Contest>()
                .Where(c => c.ParticipantType == contest.ParticipantType && c.ContestType == contest.ContestType && c.Id != contest.Id)
                .ToListAsync();
            return contests;
        }
    }
}
