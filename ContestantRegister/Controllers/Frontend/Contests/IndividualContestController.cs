using System.IO;
using System.Threading.Tasks;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Individual.Commands;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Individual.Queries;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Individual.ViewModels;
using ContestantRegister.Domain;
using ContestantRegister.Framework.Cqrs;
using ContestantRegister.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContestantRegister.Controllers
{
    public class IndividualContestController : ContestControllerBase
    {
        public IndividualContestController(IHandlerDispatcher handlerDispatcher): base(handlerDispatcher)
        {
        }
        
        /// <summary>
        /// Метод не меняет состояние регистрации. Если было ожидание подтверждения, то ожидание и остается. Или лучше сразу подтверждать после изменения статуса?
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditRegistration(int id, EditIndividualContestRegistrationViewModel viewModel)
        {
            try
            {
                EditIndividualContestRegistrationCommand command;
                if (User.IsInRole(Roles.Admin))
                    command = new EditAdminIndividualContestRegistrationCommand();
                else
                    command = new EditUserIndividualContestRegistrationCommand();
                
                command.ViewModel = viewModel;
                command.RegistrationId = id;
                await HandlerDispatcher.ExecuteCommandAsync(command);
            }
            catch (ValidationException e)
            {
                e.ValidationResult.ForEach(res => ModelState.AddModelError(res.Key, res.Value));

                await FillViewDataForContestRegistrationAsync(viewModel);

                return View(viewModel);
            }

            return RedirectToAction(nameof(Details), new { id = viewModel.ContestId });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Register(int id, CreateIndividualContestRegistrationViewModel viewModel)
        {
            try
            {
                ICommand command;
                if (User.IsInRole(Roles.Admin))
                    command = new CreateAdminIndividualContestRegistrationCommand { ViewModel = viewModel };
                else 
                    command = new CreateUserIndividualContestRegistrationCommand { ViewModel = viewModel };
                await HandlerDispatcher.ExecuteCommandAsync(command);
            }
            catch (ValidationException e)
            {
                e.ValidationResult.ForEach(res => ModelState.AddModelError(res.Key, res.Value));

                await FillViewDataForContestRegistrationAsync(viewModel);

                return View(viewModel);
            }
            
            if (viewModel.ShowRegistrationInfo)
            {
                //TODO стоит ли показывать эту страницу для тренера?
                return RedirectToAction(nameof(Registration)/*, new { id = result.RegistrationId }*/);
            }

            return RedirectToAction(nameof(Details), new { id = viewModel.ContestId });
        }
        
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ExportParticipants(int id)
        {
            var res = await HandlerDispatcher.ExecuteQueryAsync(new GetExportedIndividualContestParticipantsQuery { ContestId = id });

            var ms = new MemoryStream();
            res.ExcelPackage.SaveAs(ms);
            ms.Position = 0;
            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{res.ContestName}.xlsx");
        }
    }
}
