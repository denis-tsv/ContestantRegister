using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Team.Commands;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Team.ViewModels;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.DomainServices.ContestRegistration;
using ContestantRegister.Services.InfrastructureServices;
using Microsoft.AspNetCore.Identity;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Team.CommandHandlers
{
    internal class EditUserTeamContestRegistrationCommandHandler : EditTeamContestRegistrationCommandHandler<EditUserTeamContestRegistrationCommand>
    {
        public EditUserTeamContestRegistrationCommandHandler(
            IRepository repository, 
            IContestRegistrationService contestRegistrationService, 
            IMapper mapper, 
            ICurrentUserService currentUserService,
            UserManager<ApplicationUser> userManager) : 
            base(repository, contestRegistrationService, mapper, currentUserService, userManager)
        {
            
        }

        protected override async Task<List<KeyValuePair<string, string>>> ValidateEditTeamContestRegistrationAsync(EditTeamContestRegistrationViewModel viewModel)
        {
            var res = await base.ValidateEditTeamContestRegistrationAsync(viewModel);
            var memberRes = _contestRegistrationService.ValidateTeamContestRegistrationMember(viewModel);
            res.AddRange(memberRes);
            return res;
        }
    }
}
