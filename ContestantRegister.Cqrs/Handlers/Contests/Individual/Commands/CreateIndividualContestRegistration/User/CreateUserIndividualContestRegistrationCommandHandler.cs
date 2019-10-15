using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Individual.Commands;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Individual.ViewModels;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.DomainServices.ContestRegistration;
using ContestantRegister.Services.InfrastructureServices;
using Microsoft.AspNetCore.Identity;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Individual.CommandHandlers
{
    internal class CreateUserIndividualContestRegistrationCommandHandler : CreateIndividualContestRegistrationCommandHandler<CreateUserIndividualContestRegistrationCommand>
    {
        public CreateUserIndividualContestRegistrationCommandHandler(
            IRepository repository, 
            IEmailSender emailSender, 
            ICurrentUserService currentUserService, 
            UserManager<ApplicationUser> userManager,
            IContestRegistrationService contestRegistrationService,
            IMapper mapper) 
            : base(repository, contestRegistrationService, emailSender, currentUserService, userManager, mapper)
        {
            
        }

        protected override async Task<List<KeyValuePair<string, string>>> ValidateCreateIndividualContestRegistrationAsync(CreateIndividualContestRegistrationViewModel viewModel)
        {
            var baseRes = await base.ValidateCreateIndividualContestRegistrationAsync(viewModel);
            var memberRes = _contestRegistrationService.ValidateIndividualContestMember(viewModel);
            baseRes.AddRange(memberRes);
            return baseRes;
        }

        
    }
}
