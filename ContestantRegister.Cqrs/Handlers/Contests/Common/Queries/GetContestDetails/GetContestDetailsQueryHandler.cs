using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFilter;
using ContestantRegister.Cqrs.Features._Common.QueryHandlers;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Queries;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.Exceptions;
using ContestantRegister.Services.Extensions;
using ContestantRegister.Services.InfrastructureServices;
using Microsoft.AspNetCore.Identity;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.QueryHandlers
{
    internal class GetContestDetailsQueryHandler : ReadRepositoryQueryHandler<GetContestDetailsQuery, ContestInfoViewModelBase>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetContestDetailsQueryHandler(IReadRepository repository, ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager) : base(repository)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
        }

        public override async Task<ContestInfoViewModelBase> HandleAsync(GetContestDetailsQuery query)
        {
            var contest = await ReadRepository.Set<Contest>().SingleOrDefaultAsync(m => m.Id == query.ContestId);
            if (contest == null) throw new EntityNotFoundException();
            
            var user = await _userManager.FindByEmailAsync(_currentUserService.Email);
            ICollection<IndividualContestRegistration> userIndividualRegistrations = new List<IndividualContestRegistration>();
            ICollection<TeamContestRegistration> userTeamRegistrations = new List<TeamContestRegistration>();
            if (_currentUserService.IsAuthenticated)
            {
                if (contest.ContestType == ContestType.Individual)
                {
                    userIndividualRegistrations = await ReadRepository.Set<IndividualContestRegistration>()
                        .Include(r => r.Contest)
                        .Include(r => r.Participant1)
                        .Include(r => r.Trainer)
                        .Include(r => r.Manager)
                        .Include(r => r.StudyPlace.City)
                        .Where(r => r.ContestId == query.ContestId &&
                                    (r.Participant1Id == user.Id || r.TrainerId == user.Id || r.ManagerId == user.Id))
                        .ToListAsync();
                }
                else
                {
                    userTeamRegistrations = await ReadRepository.Set<TeamContestRegistration>()
                        .Include(r => r.Contest)
                        .Include(r => r.Participant1)
                        .Include(r => r.Participant2)
                        .Include(r => r.Participant3)
                        .Include(r => r.Trainer)
                        .Include(r => r.Manager)
                        .Include(r => r.StudyPlace.City)
                        .Where(r => r.ContestId == query.ContestId &&
                                    (r.Participant1Id == user.Id ||
                                     r.Participant2Id == user.Id ||
                                     r.Participant3Id == user.Id ||
                                     r.TrainerId == user.Id ||
                                     r.ManagerId == user.Id))
                        .ToListAsync();
                }
            }

            IEnumerable<ContestRegistration> contestRegistrations = await GetContestRegistrationsAsync(query.ContestId, contest.ContestType);

            contestRegistrations = contestRegistrations.AutoFilter(query.Filter);

            ContestInfoViewModelBase viewModel;
            if (contest.ContestType == ContestType.Individual)
            {
                viewModel = new IndividualContestInfoViewModel
                {
                    Contest = contest,
                    ContestRegistrations = contestRegistrations.Cast<IndividualContestRegistration>().ToList(),
                    UserRegistrations = userIndividualRegistrations.ToList(),
                    ParticipantRegistration = userIndividualRegistrations.SingleOrDefault(r => r.Participant1Id == user.Id),
                };
            }
            else
            {
                viewModel = new TeamContestInfoViewModel
                {
                    Contest = contest,
                    ContestRegistrations = contestRegistrations.Cast<TeamContestRegistration>().ToList(),
                    UserRegistrations = userTeamRegistrations.ToList(),
                    ParticipantRegistration = userTeamRegistrations.SingleOrDefault(r => (r.Participant1Id == user.Id || r.Participant2Id == user.Id || r.Participant3Id == user.Id) && r.Status == ContestRegistrationStatus.Completed),
                };
            }

            return viewModel;
        }

        private async Task<List<ContestRegistration>> GetContestRegistrationsAsync(int contestId, ContestType contestType)
        {
            if (contestType == ContestType.Collegiate)
            {
                return await ReadRepository.Set<TeamContestRegistration>()
                    .Include(r => r.Contest)
                    .Include(r => r.Participant1)
                    .Include(r => r.Participant2)
                    .Include(r => r.Participant3)
                    .Include(r => r.ReserveParticipant)
                    .Include(r => r.Trainer)
                    .Include(r => r.Manager)
                    .Include(r => r.StudyPlace.City)
                    .Include(r => r.ContestArea.Area)
                    .Where(r => r.ContestId == contestId)
                    .Cast<ContestRegistration>()
                    .ToListAsync();
            }

            return await ReadRepository.Set<IndividualContestRegistration>()
                .Include(r => r.Contest)
                .Include(r => r.Participant1)
                .Include(r => r.Trainer)
                .Include(r => r.Manager)
                .Include(r => r.StudyPlace.City)
                .Include(r => r.ContestArea.Area)
                .Where(r => r.ContestId == contestId)
                .Cast<ContestRegistration>()
                .ToListAsync();
        }
    }
}
