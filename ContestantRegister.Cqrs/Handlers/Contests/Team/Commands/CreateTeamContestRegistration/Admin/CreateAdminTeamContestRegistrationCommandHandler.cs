using AutoMapper;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Team.Commands;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.DomainServices.ContestRegistration;
using ContestantRegister.Services.InfrastructureServices;
using Microsoft.AspNetCore.Identity;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Team.CommandHandlers
{
    internal class CreateAdminTeamContestRegistrationCommandHandler : CreateTeamContestRegistrationCommandHandler<CreateAdminTeamContestRegistrationCommand>
    {
        public CreateAdminTeamContestRegistrationCommandHandler(
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
        
    }
}
