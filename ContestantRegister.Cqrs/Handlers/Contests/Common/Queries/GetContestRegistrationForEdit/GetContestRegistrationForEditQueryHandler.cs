using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContestantRegister.Cqrs.Features._Common.QueryHandlers;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Queries;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Individual.ViewModels;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Team.ViewModels;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.Exceptions;
using ContestantRegister.Services.Extensions;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.QueryHandlers
{
    internal class GetContestRegistrationForEditQueryHandler : ReadRepositoryQueryHandler<GetContestRegistrationForEditQuery, ContestRegistrationViewModel>
    {
        private readonly IMapper _mapper;

        public GetContestRegistrationForEditQueryHandler(IReadRepository repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public override async Task<ContestRegistrationViewModel> HandleAsync(GetContestRegistrationForEditQuery query)
        {
            var contestType = await ReadRepository.Set<ContestRegistration>().Include(x => x.Contest)
                .Where(x => x.Id == query.RegistrationId)
                .Select(x => x.Contest.ContestType)
                .SingleOrDefaultAsync();

            ContestRegistration registration = null;
            if (contestType == ContestType.Individual)
            {
                registration = await GetIndividualContestRegistrationForEditAsync(query.RegistrationId);
            }
            if (contestType == ContestType.Collegiate)
            {
                registration = await GetTeamContestRegistrationForEditAsync(query.RegistrationId);
            }

            if (registration == null) throw new EntityNotFoundException();
            
            var viewModel = CreateEditContestRegistrationViewModel(contestType);
            viewModel.ContestName = registration.Contest.Name;
            viewModel.ContestTrainerCont = registration.Contest.TrainerCount;
            viewModel.IsAreaRequired = registration.Contest.IsAreaRequired;
            viewModel.IsProgrammingLanguageNeeded = registration.Contest.IsProgrammingLanguageNeeded;
            viewModel.IsOutOfCompetitionAllowed = registration.Contest.IsOutOfCompetitionAllowed;
            viewModel.RegistrationId = registration.Id;
            viewModel.ParticipantType = registration.Contest.ParticipantType;
            viewModel.CityId = registration.StudyPlace.CityId;
            viewModel.IsEnglishLanguage = registration.Contest.IsEnglishLanguage;

            IniteEditContestRegistrationViewModel(viewModel, registration);

            _mapper.Map(registration, viewModel);

            viewModel.Status = viewModel.CheckRegistrationStatus();

            //Выставлять RegistredBy надо после маппинга, а то шибко умный маппер в поле RegistredByName кладет значение RegistredBy.Name, фамилия и email пропадают
            if (registration.RegistredBy != null)
            {
                viewModel.RegistredByName = $"{registration.RegistredBy.Name} {registration.RegistredBy.Surname} ({registration.RegistredBy.Email})";
            }

            return viewModel;
        }

        private ContestRegistrationViewModel CreateEditContestRegistrationViewModel(ContestType contestType)
        {
            if (contestType == ContestType.Individual) return new EditIndividualContestRegistrationViewModel();

            return new EditTeamContestRegistrationViewModel();
        }

        private void IniteEditContestRegistrationViewModel(ContestRegistrationViewModel viewModel, ContestRegistration registration)
        {
            if (viewModel is EditIndividualContestRegistrationViewModel individualVM)
            {
                individualVM.ParticipantName =
                    $"{registration.Participant1.Name} {registration.Participant1.Surname} ({registration.Participant1.Email})";
            }
        }

        private async Task<ContestRegistration> GetIndividualContestRegistrationForEditAsync(int registrationId)
        {
            var registration = await ReadRepository.Set<ContestRegistration>()
                //.Include(x => x.Contest).ThenInclude(y => y.ContestAreas).ThenInclude(z => z.Area)
                .Include($"{nameof(ContestRegistration.Contest)}.{nameof(Contest.ContestAreas)}.{nameof(ContestArea.Area)}")
                .Include(r => r.StudyPlace)
                .Include(r => r.RegistredBy)
                .Include(r => r.Participant1)
                .SingleOrDefaultAsync(r => r.Id == registrationId);

            return registration;
        }

        private async Task<ContestRegistration> GetTeamContestRegistrationForEditAsync(int registrationId)
        {
            var registration = await ReadRepository.Set<TeamContestRegistration>()
                //.Include(x => x.Contest).ThenInclude(y => y.ContestAreas).ThenInclude(z => z.Area)
                .Include($"{nameof(ContestRegistration.Contest)}.{nameof(Contest.ContestAreas)}.{nameof(ContestArea.Area)}")
                .Include(r => r.StudyPlace)
                .Include(r => r.RegistredBy)
                .Include(r => r.Participant1)
                .Include(r => r.Participant2)
                .Include(r => r.Participant3)
                .Include(r => r.ReserveParticipant)
                .SingleOrDefaultAsync(r => r.Id == registrationId);

            return registration;
        }
    }
}
