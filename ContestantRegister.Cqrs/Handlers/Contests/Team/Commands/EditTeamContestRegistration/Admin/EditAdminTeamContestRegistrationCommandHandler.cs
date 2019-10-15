using AutoMapper;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Team.Commands;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.DomainServices.ContestRegistration;
using ContestantRegister.Services.InfrastructureServices;
using Microsoft.AspNetCore.Identity;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Team.CommandHandlers
{
    internal class EditAdminTeamContestRegistrationCommandHandler : EditTeamContestRegistrationCommandHandler<EditAdminTeamContestRegistrationCommand>
    {
        public EditAdminTeamContestRegistrationCommandHandler(
            IRepository repository, 
            IContestRegistrationService contestRegistrationService, 
            IMapper mapper, 
            ICurrentUserService currentUserService,
            UserManager<ApplicationUser> userManager) : 
            base(repository, contestRegistrationService, mapper, currentUserService, userManager)
        {
            
        }
    }
}
