using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContestantRegister.Cqrs.Features._Common.QueryHandlers;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Queries;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.Exceptions;
using ContestantRegister.Services.Extensions;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.QueryHandlers
{
    internal class SortingQueryHandler : ReadRepositoryQueryHandler<SortingQuery, SortingQueryResult>
    {
        private readonly IMapper _mapper;

        public SortingQueryHandler(IReadRepository repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public override async Task<SortingQueryResult> HandleAsync(SortingQuery query)
        {
            var contest = await ReadRepository.Set<Contest>()
                .Include(x => x.ContestAreas)
                //.Include($"{nameof(Contest.ContestAreas)}.{nameof(ContestArea.Area)}")
                //.Include($"{nameof(Contest.ContestRegistrations)}.{nameof(ContestRegistration.Participant1)}")
                //.Include($"{nameof(Contest.ContestRegistrations)}.{nameof(ContestRegistration.StudyPlace)}")
                .SingleOrDefaultAsync(m => m.Id == query.ContestId);

            if (contest == null) throw new EntityNotFoundException();

            var compClassIds = contest.ContestAreas
                .Where(ca => !string.IsNullOrEmpty(ca.SortingCompClassIds))
                .SelectMany(c => c.SortingCompClassIds.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .Select(int.Parse)
                .ToArray();

            var viewModel = new SortingViewModel();
            _mapper.Map(contest, viewModel);

            var res = new SortingQueryResult
            {
                ViewModel = viewModel,
                CompClassIds = compClassIds,
            };

            return res;
        }
    }
}
