﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ContestantRegister.Data;
using ContestantRegister.Models;
using ContestantRegister.Utils;
using ContestantRegister.ViewModels.Contest.Registration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ContestantRegister.Services
{
    public interface IContestRegistrationService
    {
        Task<List<KeyValuePair<string, string>>> ValidateCreateIndividualContestRegistrationAsync(CreateIndividualContestRegistrationViewModel viewModel, ClaimsPrincipal user);
        Task<List<KeyValuePair<string, string>>> ValidateEditIndividualContestRegistrationAsync(EditIndividualContestRegistrationViewModel viewModel, ClaimsPrincipal user);

        Task<List<KeyValuePair<string, string>>> ValidateCreateTeamContestRegistrationAsync(CreateTeamContestRegistrationViewModel viewModel, ClaimsPrincipal user);
        Task<List<KeyValuePair<string, string>>> ValidateEditTeamContestRegistrationAsync(EditTeamContestRegistrationViewModel viewModel, ClaimsPrincipal user);
    }

    public class ContestRegistrationService : IContestRegistrationService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContestRegistrationService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<List<KeyValuePair<string, string>>> ValidateIndividualContestRegistrationAsync(IndividualContestRegistrationViewModel viewModel, ClaimsPrincipal user, bool editRegistration)
        {
            var result = new List<KeyValuePair<string, string>>();

            var participantExists = !string.IsNullOrEmpty(viewModel.Participant1Id);
            if (!participantExists)
                result.Add(KeyValuePair.Create(nameof(viewModel.Participant1Id), viewModel.GetRequredFieldErrorMessage(nameof(viewModel.Participant1Id))));

            if (editRegistration && !viewModel.Number.HasValue)
                result.Add(KeyValuePair.Create(nameof(viewModel.Number), viewModel.GetRequredFieldErrorMessage(nameof(viewModel.Number))));

            var contest = await _context.Contests.SingleAsync(c => c.Id == viewModel.ContestId);

            if (contest.IsProgrammingLanguageNeeded && string.IsNullOrEmpty(viewModel.ProgrammingLanguage))
                result.Add(KeyValuePair.Create(nameof(viewModel.ProgrammingLanguage), viewModel.GetRequredFieldErrorMessage(nameof(viewModel.ProgrammingLanguage))));

            var studyPlace = await _context.StudyPlaces.SingleAsync(s => s.Id == viewModel.StudyPlaceId);
            if (!viewModel.IsOutOfCompetition &&
                    (contest.ParticipantType == ParticipantType.Pupil && studyPlace is Institution ||
                     contest.ParticipantType == ParticipantType.Student && studyPlace is School)
                )
                result.Add(KeyValuePair.Create(nameof(viewModel.StudyPlaceId), "Тип учебного заведения не соответствует типу контеста"));
            if (viewModel.CityId != studyPlace.CityId)
                result.Add(KeyValuePair.Create(nameof(viewModel.CityId), "Выбранный город не соответствует городу учебного заведения"));

            if (editRegistration)
            {
                var dbRegistration = await _context.ContestRegistrations.Include(r => r.Participant1).SingleAsync(r => r.Id == viewModel.RegistrationId);
                if (dbRegistration.Participant1Id != viewModel.Participant1Id)
                    result.Add(KeyValuePair.Create(nameof(viewModel.Participant1Id), "Нельзя менять пользователя при изменении регистрации"));
            }
            else
            {
                var participantRegistred = await _context.ContestRegistrations.AnyAsync(r => r.ContestId == contest.Id && r.Participant1Id == viewModel.Participant1Id);
                if (participantRegistred) result.Add(KeyValuePair.Create(nameof(viewModel.Participant1Id), "Указанный участник уже зарегистрирован в контесте"));
            }

            if (viewModel.Participant1Id == viewModel.TrainerId) result.Add(KeyValuePair.Create(nameof(viewModel.TrainerId), "Участник не может быть своим тренером"));
            if (viewModel.Participant1Id == viewModel.ManagerId) result.Add(KeyValuePair.Create(nameof(viewModel.ManagerId), "Участник не может быть своим руководителем"));

            var currentUser = await _userManager.GetUserAsync(user);
            if (!user.IsInRole(Roles.Admin) && viewModel.Participant1Id != currentUser.Id && viewModel.TrainerId != currentUser.Id && viewModel.ManagerId != currentUser.Id)
                result.Add(KeyValuePair.Create(string.Empty, "Вы должны быть участником, тренером или руководителем, чтобы завершить регистрацию"));

            var participant = await _context.Users.SingleOrDefaultAsync(u => u.Id == viewModel.Participant1Id);
            var trainer = await _context.Users.SingleAsync(u => u.Id == viewModel.TrainerId);
            var manager = await _context.Users.SingleOrDefaultAsync(u => u.Id == viewModel.ManagerId);

            if (contest.ParticipantType == ParticipantType.Pupil && !viewModel.IsOutOfCompetition)
            {
                if (participantExists && participant.UserType != UserType.Pupil)
                {
                    result.Add(KeyValuePair.Create(nameof(viewModel.Participant1Id), "Только школьник может быть участником школьного контеста"));
                }
                if (!viewModel.Class.HasValue)
                {
                    result.Add(KeyValuePair.Create(nameof(viewModel.Class), viewModel.GetRequredFieldErrorMessage(nameof(viewModel.Class))));
                }
            }

            if (contest.ParticipantType == ParticipantType.Student && !viewModel.IsOutOfCompetition)
            {
                if (participantExists && participant.UserType != UserType.Student) result.Add(KeyValuePair.Create(nameof(viewModel.Participant1Id), "Только студент может быть участником студенческого контеста"));
                if (trainer.UserType == UserType.Pupil) result.Add(KeyValuePair.Create(nameof(viewModel.TrainerId), "Школьник не может быть тренером на студенческом контесте"));
                if (manager != null && manager.UserType == UserType.Pupil) result.Add(KeyValuePair.Create(nameof(viewModel.ManagerId), "Школьник не может быть руководителем на студенческом контесте"));

                if (!viewModel.Course.HasValue)
                    result.Add(KeyValuePair.Create(nameof(viewModel.Course), viewModel.GetRequredFieldErrorMessage(nameof(viewModel.Course))));
            }

            if (contest.IsAreaRequired && !viewModel.ContestAreaId.HasValue)
                result.Add(KeyValuePair.Create(nameof(viewModel.ContestAreaId), viewModel.GetRequredFieldErrorMessage(nameof(viewModel.ContestAreaId))));

            return result;
        }

        public Task<List<KeyValuePair<string, string>>> ValidateCreateIndividualContestRegistrationAsync(CreateIndividualContestRegistrationViewModel viewModel, ClaimsPrincipal user)
        {
            return ValidateIndividualContestRegistrationAsync(viewModel, user, false);
        }

        public Task<List<KeyValuePair<string, string>>> ValidateEditIndividualContestRegistrationAsync(EditIndividualContestRegistrationViewModel viewModel, ClaimsPrincipal user)
        {
            return ValidateIndividualContestRegistrationAsync(viewModel, user, true);
        }

        public Task<List<KeyValuePair<string, string>>> ValidateCreateTeamContestRegistrationAsync(CreateTeamContestRegistrationViewModel viewModel, ClaimsPrincipal user)
        {
            return ValidateTeamContestRegistrationAsync(viewModel, user, false);
        }

        public Task<List<KeyValuePair<string, string>>> ValidateEditTeamContestRegistrationAsync(EditTeamContestRegistrationViewModel viewModel, ClaimsPrincipal user)
        {
            return ValidateTeamContestRegistrationAsync(viewModel, user, true);
        }

        private async Task<List<KeyValuePair<string, string>>> ValidateTeamContestRegistrationAsync(TeamContestRegistrationViewModel viewModel, ClaimsPrincipal user, bool editRegistration)
        {
            var result = new List<KeyValuePair<string, string>>();

            
            if (editRegistration && !viewModel.Number.HasValue)
                result.Add(KeyValuePair.Create(nameof(viewModel.Number), viewModel.GetRequredFieldErrorMessage(nameof(viewModel.Number))));

            var contest = await _context.Contests.SingleAsync(c => c.Id == viewModel.ContestId);

            if (contest.IsProgrammingLanguageNeeded && string.IsNullOrEmpty(viewModel.ProgrammingLanguage))
                result.Add(KeyValuePair.Create(nameof(viewModel.ProgrammingLanguage), viewModel.GetRequredFieldErrorMessage(nameof(viewModel.ProgrammingLanguage))));

            var studyPlace = await _context.StudyPlaces.SingleAsync(s => s.Id == viewModel.StudyPlaceId);
            if (!viewModel.IsOutOfCompetition)
            {
                if (contest.ParticipantType == ParticipantType.Pupil && studyPlace is Institution)
                    result.Add(KeyValuePair.Create(nameof(viewModel.StudyPlaceId), "Поставьте флажок 'Вне конкурса' для регистрации студенческой команды на школьное соревнование"));
                if (contest.ParticipantType == ParticipantType.Student && studyPlace is School)
                    result.Add(KeyValuePair.Create(nameof(viewModel.StudyPlaceId), "Поставьте флажок 'Вне конкурса' для регистрации школьной команды на студенческое соревнование"));
            }

            if (viewModel.CityId != studyPlace.CityId)
            {
                result.Add(KeyValuePair.Create(nameof(viewModel.CityId), "Выбранный город не соответствует городу учебного заведения"));
            }

            var participant1Exists = !string.IsNullOrEmpty(viewModel.Participant1Id);
            if (participant1Exists)
            {
                var participant1Team = await ParticipantExistsInOtherTeams(viewModel.RegistrationId, contest, viewModel.Participant1Id);
                if (participant1Team != null) result.Add(KeyValuePair.Create(nameof(viewModel.Participant1Id), $"Участник уже зарегистрирован в команде '{participant1Team.DisplayTeamName}'"));
                var participant1TrainerTeam = await TrainerExistsInOtherTeams(viewModel.RegistrationId, contest, viewModel.Participant1Id);
                if (participant1TrainerTeam != null) result.Add(KeyValuePair.Create(nameof(viewModel.Participant1Id), $"Участник уже зарегистрирован как тренер в команде '{participant1TrainerTeam.DisplayTeamName}'"));
            }

            var participant2Exists = !string.IsNullOrEmpty(viewModel.Participant2Id);
            if (participant2Exists)
            {
                var participant2Team = await ParticipantExistsInOtherTeams(viewModel.RegistrationId, contest, viewModel.Participant2Id);
                if (participant2Team != null) result.Add(KeyValuePair.Create(nameof(viewModel.Participant2Id), $"Участник уже зарегистрирован в команде '{participant2Team.DisplayTeamName}'"));
                var participant2TrainerTeam = await TrainerExistsInOtherTeams(viewModel.RegistrationId, contest, viewModel.Participant2Id);
                if (participant2TrainerTeam != null) result.Add(KeyValuePair.Create(nameof(viewModel.Participant2Id), $"Участник уже зарегистрирован как тренер в команде '{participant2TrainerTeam.DisplayTeamName}'"));
            }

            var participant3Exists = !string.IsNullOrEmpty(viewModel.Participant3Id);
            if (participant3Exists)
            {
                var participant3Team = await ParticipantExistsInOtherTeams(viewModel.RegistrationId, contest, viewModel.Participant3Id);
                if (participant3Team != null) result.Add(KeyValuePair.Create(nameof(viewModel.Participant3Id), $"Участник уже зарегистрирован в команде '{participant3Team.DisplayTeamName}'"));
                var participant3TrainerTeam = await TrainerExistsInOtherTeams(viewModel.RegistrationId, contest, viewModel.Participant3Id);
                if (participant3TrainerTeam != null) result.Add(KeyValuePair.Create(nameof(viewModel.Participant3Id), $"Участник уже зарегистрирован как тренер в команде '{participant3TrainerTeam.DisplayTeamName}'"));
            }
            
            var reserveParticipantExists = !string.IsNullOrEmpty(viewModel.ReserveParticipantId);
            if (reserveParticipantExists)
            {
                var reserveParticipantTeam = await ParticipantExistsInOtherTeams(viewModel.RegistrationId, contest, viewModel.ReserveParticipantId);
                if (reserveParticipantTeam != null) result.Add(KeyValuePair.Create(nameof(viewModel.ReserveParticipantId), $"Участник уже зарегистрирован в команде '{reserveParticipantTeam.DisplayTeamName}'"));

                var reserveTrainerTeam = await TrainerExistsInOtherTeams(viewModel.RegistrationId, contest, viewModel.ReserveParticipantId);
                if (reserveTrainerTeam != null) result.Add(KeyValuePair.Create(nameof(viewModel.ReserveParticipantId), $"Участник уже зарегистрирован как тренер в кманде '{reserveTrainerTeam.DisplayTeamName}'"));
            }

            //TODO валидация для тренера 2 и 3 

            var trainerParticipantTeam = await ParticipantExistsInOtherTeams(viewModel.RegistrationId, contest, viewModel.TrainerId);
            if (trainerParticipantTeam != null) result.Add(KeyValuePair.Create(nameof(viewModel.TrainerId), $"Тренер уже зарегистрирован как участник в команде {trainerParticipantTeam.DisplayTeamName}"));

            var idNameDictionary = new Dictionary<string, string>();

            string propertyName;

            if (participant1Exists)
            {
                idNameDictionary.Add(viewModel.Participant1Id, nameof(viewModel.Participant1Id));                
            }

            if (participant2Exists)
            {
                if (idNameDictionary.TryGetValue(viewModel.Participant2Id, out propertyName))
                {
                    result.Add(KeyValuePair.Create(propertyName, "Участник указан дважды"));
                    result.Add(KeyValuePair.Create(nameof(viewModel.Participant2Id), "Участник указан дважды"));
                }
                else
                {
                    idNameDictionary.Add(viewModel.Participant2Id, nameof(viewModel.Participant2Id));
                }
            }

            if (participant3Exists)
            {
                if (idNameDictionary.TryGetValue(viewModel.Participant3Id, out propertyName))
                {
                    result.Add(KeyValuePair.Create(propertyName, "Участник указан дважды"));
                    result.Add(KeyValuePair.Create(nameof(viewModel.Participant3Id), "Участник указан дважды"));
                }
                else
                {
                    idNameDictionary.Add(viewModel.Participant3Id, nameof(viewModel.Participant3Id));
                }
            }

            if (reserveParticipantExists)
            {
                if (idNameDictionary.TryGetValue(viewModel.ReserveParticipantId, out propertyName))
                {
                    result.Add(KeyValuePair.Create(propertyName, "Участник указан дважды"));
                    result.Add(KeyValuePair.Create(nameof(viewModel.ReserveParticipantId), "Участник указан дважды"));
                }
                else
                {
                    idNameDictionary.Add(viewModel.ReserveParticipantId, nameof(viewModel.ReserveParticipantId));
                }
            }

            if (viewModel.Participant1Id == viewModel.TrainerId ||
                viewModel.Participant2Id == viewModel.TrainerId ||
                viewModel.Participant3Id == viewModel.TrainerId ||
                (reserveParticipantExists && viewModel.ReserveParticipantId == viewModel.TrainerId))
            {
                result.Add(KeyValuePair.Create(nameof(viewModel.TrainerId), "Участник не может быть тренером"));
            }

            if (!string.IsNullOrEmpty(viewModel.ManagerId) &&
                    (
                        viewModel.Participant1Id == viewModel.ManagerId ||
                        viewModel.Participant2Id == viewModel.ManagerId ||
                        viewModel.Participant3Id == viewModel.ManagerId ||
                        (reserveParticipantExists && viewModel.ReserveParticipantId == viewModel.ManagerId)
                    )
                )
            {
                result.Add(KeyValuePair.Create(nameof(viewModel.ManagerId), "Участник не может быть руководителем"));
            }

            var currentUser = await _userManager.GetUserAsync(user);
            if (!user.IsInRole(Roles.Admin) && 
                viewModel.Participant1Id != currentUser.Id &&
                viewModel.Participant2Id != currentUser.Id &&
                viewModel.Participant3Id != currentUser.Id &&
                viewModel.ReserveParticipantId != currentUser.Id &&
                viewModel.TrainerId != currentUser.Id && 
                viewModel.ManagerId != currentUser.Id)
            {
                result.Add(KeyValuePair.Create(string.Empty, "Вы должны быть участником, тренером или руководителем, чтобы завершить регистрацию"));
            }

            var participant1 = await _context.Users.SingleOrDefaultAsync(u => u.Id == viewModel.Participant1Id);
            var participant2 = await _context.Users.SingleOrDefaultAsync(u => u.Id == viewModel.Participant2Id);
            var participant3 = await _context.Users.SingleOrDefaultAsync(u => u.Id == viewModel.Participant3Id);
            var trainer = await _context.Users.SingleAsync(u => u.Id == viewModel.TrainerId);
            var manager = await _context.Users.SingleOrDefaultAsync(u => u.Id == viewModel.ManagerId);
            ApplicationUser reserveParticipant = null;
            if (reserveParticipantExists)
                reserveParticipant = await _context.Users.SingleAsync(u => u.Id == viewModel.ReserveParticipantId);

            if (contest.ParticipantType == ParticipantType.Pupil && !viewModel.IsOutOfCompetition)
            {
                var message = "Только школьник может быть участником школьного контеста";
                if (participant1 != null && participant1.UserType != UserType.Pupil) result.Add(KeyValuePair.Create(nameof(viewModel.Participant1Id), message));
                if (participant2 != null && participant2.UserType != UserType.Pupil) result.Add(KeyValuePair.Create(nameof(viewModel.Participant2Id), message));
                if (participant3 != null && participant3.UserType != UserType.Pupil) result.Add(KeyValuePair.Create(nameof(viewModel.Participant3Id), message));
                if (reserveParticipant != null && reserveParticipant.UserType != UserType.Pupil) result.Add(KeyValuePair.Create(nameof(viewModel.ReserveParticipantId), message));
            }

            if (contest.ParticipantType == ParticipantType.Student && !viewModel.IsOutOfCompetition)
            {
                var message = "Чтобы зарегистрировать участника - не студента, поставьте флаг 'Вне конкурса'";
                if (participant1 != null && participant1.UserType != UserType.Student) result.Add(KeyValuePair.Create(nameof(viewModel.Participant1Id), message));
                if (participant2 != null && participant2.UserType != UserType.Student) result.Add(KeyValuePair.Create(nameof(viewModel.Participant2Id), message));
                if (participant3 != null && participant3.UserType != UserType.Student) result.Add(KeyValuePair.Create(nameof(viewModel.Participant3Id), message));
                if (reserveParticipant != null && reserveParticipant.UserType != UserType.Student) result.Add(KeyValuePair.Create(nameof(viewModel.ReserveParticipantId), message));
                if (trainer.UserType == UserType.Pupil) result.Add(KeyValuePair.Create(nameof(viewModel.TrainerId), "Школьник не может быть тренером на студенческом контесте"));
                if (manager != null && manager.UserType == UserType.Pupil) result.Add(KeyValuePair.Create(nameof(viewModel.ManagerId), "Школьник не может быть руководителем на студенческом контесте"));
            }

            if (contest.IsAreaRequired && !viewModel.ContestAreaId.HasValue)
            {
                result.Add(KeyValuePair.Create(nameof(viewModel.ContestAreaId), viewModel.GetRequredFieldErrorMessage(nameof(viewModel.ContestAreaId))));
            }

            if (contest.IsEnglishLanguage && !string.IsNullOrEmpty(viewModel.TeamName) &&
                !Regex.IsMatch(viewModel.TeamName, "^[ a-zA-Z0-9]{0,100}$"))
            {
                result.Add(KeyValuePair.Create(nameof(viewModel.TeamName), "Только английские буквы, цифры и пробел"));
            }

            if (!participant1Exists && !participant2Exists && !participant3Exists)
                result.Add(KeyValuePair.Create(nameof(viewModel), "Нельзя регистрировать команду без участников"));

            viewModel.Status = viewModel.CheckRegistrationStatus();
            
            return result;
        }

        private Task<TeamContestRegistration> ParticipantExistsInOtherTeams(int registrationId, Contest contest, string participantId)
        {
            return _context.TeamContestRegistrations.FirstOrDefaultAsync(r =>
                r.Id != registrationId &&
                r.ContestId == contest.Id &&                
                (r.Participant1Id == participantId ||
                 r.Participant2Id == participantId ||
                 r.Participant3Id == participantId));
        }

        private Task<TeamContestRegistration> TrainerExistsInOtherTeams(int registrationId, Contest contest, string trainerId)
        {
            return _context.TeamContestRegistrations.FirstOrDefaultAsync(r =>
                r.Id != registrationId &&
                r.ContestId == contest.Id &&                
                r.TrainerId == trainerId);
        }
    }
}
