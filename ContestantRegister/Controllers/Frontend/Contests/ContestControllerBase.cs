using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Commands;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Queries;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels.SelectedListItem;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Individual.ViewModels;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Team.Queries;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Team.ViewModels;
using ContestantRegister.Domain;
using ContestantRegister.Framework.Cqrs;
using ContestantRegister.Models;
using ContestantRegister.Services.Exceptions;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValidationException = ContestantRegister.Services.Exceptions.ValidationException;

namespace ContestantRegister.Controllers
{
    public abstract class ContestControllerBase : Controller
    {
        protected readonly IHandlerDispatcher HandlerDispatcher;
        protected ContestControllerBase(IHandlerDispatcher handlerDispatcher)
        {
            HandlerDispatcher = handlerDispatcher;
        }

        public async Task<IActionResult> Details(int id, ContestParticipantFilter filter) //TODO как переименовать парамерт в contestId? Какой-то маппинг надо подставить
        {
            ViewData["ParticipantName"] = filter.ParticipantName;
            ViewData["TrainerName"] = filter.TrainerName;
            ViewData["ManagerName"] = filter.ManagerName;
            ViewData["City"] = filter.City;
            ViewData["Area"] = filter.Area;
            ViewData["StudyPlace"] = filter.StudyPlace;
            ViewData["Status"] = filter.Status;

            var viewModel = await HandlerDispatcher.ExecuteQueryAsync(new GetContestDetailsQuery{ContestId = id, Filter = filter});
            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Register(int id) //TODO как переименовать парамерт в contestId? Какой-то маппинг надо подставить
        {
            var viewModel = await HandlerDispatcher.ExecuteQueryAsync(new GetContestRegistrationForCreateQuery{ ContestId = id });
            await FillViewDataForContestRegistrationAsync(viewModel);
            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> EditRegistration(int id)
        {
            var viewModel = await HandlerDispatcher.ExecuteQueryAsync(new GetContestRegistrationForEditQuery{RegistrationId = id});
            await FillViewDataForContestRegistrationAsync(viewModel);
            return View(viewModel);
        }
        
        protected async Task FillViewDataForContestRegistrationAsync(ContestRegistrationViewModel viewModel)
        {
            var data = await HandlerDispatcher.ExecuteQueryAsync(new GetDataForContestRegistrationQuery{ ContestId = viewModel.ContestId } );

            ViewData["CityId"] = new SelectList(data.Cities, "Id", "Name", viewModel.CityId);
            var creatIndividualVM = viewModel as IndividualContestRegistrationViewModel;
            var creatTeamVM = viewModel as TeamContestRegistrationViewModel;
            
            if (creatIndividualVM != null)
            {
                ViewData["Participant1Id"] = new SelectList(data.Users, "Id", "DisplayName", creatIndividualVM.Participant1Id);
            }

            if (creatTeamVM != null)
            {
                ViewData["Participant1Id"] = new SelectList(data.Users, "Id", "DisplayName", creatTeamVM.Participant1Id);
                ViewData["Participant2Id"] = new SelectList(data.Users, "Id", "DisplayName", creatTeamVM.Participant2Id);
                ViewData["Participant3Id"] = new SelectList(data.Users, "Id", "DisplayName", creatTeamVM.Participant3Id);
                ViewData["ReserveParticipantId"] = new SelectList(data.Users, "Id", "DisplayName", creatTeamVM.ReserveParticipantId);

                if (data.TrainerCount > 1)
                {
                    ViewData["Trainer2Id"] = new SelectList(data.Users.Where(u => u.UserType != UserType.Pupil), "Id", "DisplayName", creatTeamVM.Trainer2Id);
                }
                if (data.TrainerCount > 2)
                {
                    ViewData["Trainer3Id"] = new SelectList(data.Users.Where(u => u.UserType != UserType.Pupil), "Id", "DisplayName", creatTeamVM.Trainer3Id);
                }
            }
            ViewData["TrainerId"] = new SelectList(data.Users.Where(u => u.UserType != UserType.Pupil), "Id", "DisplayName", viewModel.TrainerId);

            ViewData["ManagerId"] = new SelectList(data.Users.Where(u => u.UserType != UserType.Pupil), "Id", "DisplayName", viewModel.ManagerId);
            ViewData["StudyPlaces"] = data.StudyPlaces;
            
            if (data.IsAreaRequired)
            {
                ViewData["Area"] = new SelectList(data.ContestAreas, "Id", "Area.Name", viewModel.ContestAreaId);
            }
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteRegistration(int id)
        {
            //TODO На UI переспросить "Вы точно уверены, что хотите удалить регистрацию?"

            var command = new DeleteRegistrationCommand{RegistrationId = id};
            await HandlerDispatcher.ExecuteCommandAsync(command);
            return RedirectToAction(nameof(Details), new { id = command.ContestId });
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Sorting(int id, SortingViewModel viewModel)
        {
            try
            {
                await HandlerDispatcher.ExecuteCommandAsync(new SortingCommand {ContestId = id, ViewModel = viewModel});
            }
            catch (ValidationException e)
            {
                e.ValidationResult.ForEach(res => ModelState.AddModelError(res.Key, res.Value));

                var data1 = await HandlerDispatcher.ExecuteQueryAsync(new GetDataForSortingQuery { ContestId = id, SelectedContestAreaId = viewModel.SelectedContestAreaId, SelectedCompClassIds = viewModel.SelectedCompClassIds });
                FillSortingViewData(data1.ContestAreas, data1.CompClasses);

                return View(viewModel);
            }

            var data = await HandlerDispatcher.ExecuteQueryAsync(new GetDataForSortingQuery { ContestId = id, SelectedContestAreaId = viewModel.SelectedContestAreaId, SelectedCompClassIds = viewModel.SelectedCompClassIds });
            FillSortingViewData(data.ContestAreas, data.CompClasses);

            return View(viewModel);
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Sorting(int id)
        {
            var res = await HandlerDispatcher.ExecuteQueryAsync(new SortingQuery {ContestId = id});
            var data = await HandlerDispatcher.ExecuteQueryAsync(new GetDataForSortingQuery { ContestId = id, SelectedCompClassIds = res.CompClassIds });
            FillSortingViewData(data.ContestAreas, data.CompClasses);

            return View(res.ViewModel);
        }

        public async Task<IActionResult> ImportFromContest(int id)
        {
            try
            {
                var contests = await HandlerDispatcher.ExecuteQueryAsync(new ImportFromContestQuery {ContestId = id});
                ViewData["FromContestId"] = new SelectList(contests, "Id", "Name");
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError(string.Empty, "Нет аккаунтов участников в текущем контекте, чтобы туда кого-то добавлять");
            }
            
            var viewModel = new ImportContestParticipantsViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ImportFromContest(int id, ImportContestParticipantsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var contests = await HandlerDispatcher.ExecuteQueryAsync(new ImportFromContestQuery { ContestId = id });
                ViewData["FromContestId"] = new SelectList(contests, "Id", "Name", viewModel.FromContestId);
                return View(viewModel);
            }

            try
            {
                await HandlerDispatcher.ExecuteCommandAsync(new ImportFromContestCommand { ContestId = id, ViewModel = viewModel } );
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var contests = await HandlerDispatcher.ExecuteQueryAsync(new ImportFromContestQuery { ContestId = id });
                ViewData["FromContestId"] = new SelectList(contests, "Id", "Name", viewModel.FromContestId);
                return View(viewModel);
            }

            //TODO Сказать о том, что участники успешно импортированы
            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        //TODO стоит ли делать POST вместо GET?
        public async Task<IActionResult> CancelRegistration(int id)
        {
            var command = new CancelRegistrationCommand {RegistrationId = id};
            await HandlerDispatcher.ExecuteCommandAsync(command);
            return RedirectToAction(nameof(Details), new { id = command.ContestId });
        }

        [Authorize]
        //TODO стоит ли делать POST вместо GET?
        public IActionResult CompleteRegistration(int id)
        {
            return RedirectToAction(nameof(EditRegistration), new { id });
        }

        private void FillSortingViewData(List<ContestAreaSelectedListItemViewModel> contestAreas, List<CompClassSelectedListItemViewModel> compClasses)
        {
            ViewData["Areas"] = contestAreas;
            ViewData["CompClasses"] = compClasses;
        }

        [Authorize]
        public async Task<IActionResult> Registration()
        {
            var registration = await HandlerDispatcher.ExecuteQueryAsync(new GetLastRegistrationForCurrentUserQuery());
            if (registration == null) throw new EntityNotFoundException();

            return View(registration);
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ImportParticipants(int id)
        {
            var vm = await HandlerDispatcher.ExecuteQueryAsync(new ImportParticipantsQuery { ContestId = id });
            return View(vm);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> ImportParticipants(int id, ImportParticipantsViewModel viewModel)
        {
            await HandlerDispatcher.ExecuteCommandAsync(new ImportParticipantsCommand { ContestId = id, ViewModel = viewModel});
            
            return RedirectToAction(nameof(Details), new { id });
        }

    }
}
