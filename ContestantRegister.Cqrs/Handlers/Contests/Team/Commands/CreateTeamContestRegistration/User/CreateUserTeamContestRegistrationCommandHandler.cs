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
    internal class CreateUserTeamContestRegistrationCommandHandler : CreateTeamContestRegistrationCommandHandler<CreateUserTeamContestRegistrationCommand>
    {
        public CreateUserTeamContestRegistrationCommandHandler(
            IRepository repository, 
            IMapper mapper, 
            IContestRegistrationService contestRegistrationService,
            IEmailSender emailSender,
            ICurrentUserService currentUserService,
            UserManager<ApplicationUser> userManager
            ) 
            : base(repository, mapper, contestRegistrationService, emailSender, currentUserService, userManager)
        {
            
        }

        protected override async Task<List<KeyValuePair<string, string>>> ValidateCreateTeamContestRegistrationAsync(CreateTeamContestRegistrationViewModel viewModel)
        {
            var res = await base.ValidateCreateTeamContestRegistrationAsync(viewModel);
            var memberRes = _contestRegistrationService.ValidateTeamContestRegistrationMember(viewModel);
            res.AddRange(memberRes);
            return res;
        }
    }
}
