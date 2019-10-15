using System.Threading.Tasks;
using ContestantRegister.Cqrs.Features._Common.QueryHandlers;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Queries;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Individual.ViewModels;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Team.ViewModels;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.Exceptions;
using ContestantRegister.Services.Extensions;
using ContestantRegister.Services.InfrastructureServices;
using Microsoft.AspNetCore.Identity;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.QueryHandlers
{
    internal class GetContestRegistrationForCreateQueryHandler : ReadRepositoryQueryHandler<GetContestRegistrationForCreateQuery, ContestRegistrationViewModel>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetContestRegistrationForCreateQueryHandler(IReadRepository repository, ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager) : base(repository)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
        }

        public override async Task<ContestRegistrationViewModel> HandleAsync(GetContestRegistrationForCreateQuery query)
        {
            var contest = await ReadRepository.Set<Contest>()
               //.Include(x => x.ContestAreas).ThenInclude(y => y.Area)
               .Include($"{nameof(Contest.ContestAreas)}.{nameof(ContestArea.Area)}")
               .SingleOrDefaultAsync(m => m.Id == query.ContestId);
            if (contest == null)  throw new EntityNotFoundException();
            
            var viewModel = CreateContestRegistrationViewModel(contest.ContestType);
            viewModel.ContestName = contest.Name;
            viewModel.ContestId = contest.Id;
            viewModel.ContestTrainerCont = contest.TrainerCount;
            viewModel.ParticipantType = contest.ParticipantType;
            viewModel.IsAreaRequired = contest.IsAreaRequired;
            viewModel.IsProgrammingLanguageNeeded = contest.IsProgrammingLanguageNeeded;
            viewModel.IsOutOfCompetitionAllowed = contest.IsOutOfCompetitionAllowed;
            viewModel.IsEnglishLanguage = contest.IsEnglishLanguage;
            viewModel.ShowRegistrationInfo = contest.ShowRegistrationInfo;

            var user = await _userManager.FindByEmailAsync(_currentUserService.Email);
            user.StudyPlace = await ReadRepository.FindAsync<StudyPlace>(user.StudyPlaceId);
            switch (user.UserType)
            {
                case UserType.Trainer:
                    viewModel.TrainerId = user.Id;
                    break;

                case UserType.Pupil:
                    viewModel.Participant1Id = user.Id;
                    break;

                case UserType.Student:
                    if (contest.ParticipantType == ParticipantType.Pupil)
                    {
                        viewModel.TrainerId = user.Id;
                    }
                    else
                    {
                        viewModel.Participant1Id = user.Id;
                    }
                    break;
            }

            if (contest.ParticipantType == ParticipantType.Pupil && user.StudyPlace is School ||
                contest.ParticipantType == ParticipantType.Student && user.StudyPlace is Institution)
            {
                viewModel.StudyPlaceId = user.StudyPlaceId;
                viewModel.CityId = user.StudyPlace.CityId;
            }

            return viewModel;
        }

        private ContestRegistrationViewModel CreateContestRegistrationViewModel(ContestType contestType)
        {
            if (contestType == ContestType.Individual) return new CreateIndividualContestRegistrationViewModel();

            return new CreateTeamContestRegistrationViewModel();
        }
    }
}
