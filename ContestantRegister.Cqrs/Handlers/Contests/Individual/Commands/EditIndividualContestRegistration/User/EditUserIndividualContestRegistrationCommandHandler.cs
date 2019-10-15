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
    internal class EditUserIndividualContestRegistrationCommandHandler : EditIndividualContestRegistrationCommandHandler<EditUserIndividualContestRegistrationCommand>
    {
        public EditUserIndividualContestRegistrationCommandHandler(
            IRepository repository, 
            IContestRegistrationService contestRegistrationService, 
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            ICurrentUserService currentUserService
            ) 
            : base(repository, contestRegistrationService, mapper, userManager, currentUserService)
        {
        
        }

        protected override async Task<List<KeyValuePair<string, string>>> ValidateEditIndividualContestRegistrationAsync(EditIndividualContestRegistrationViewModel viewModel)
        {
            var baseRes = await base.ValidateEditIndividualContestRegistrationAsync(viewModel);
            var memberRes = _contestRegistrationService.ValidateIndividualContestMember(viewModel);
            baseRes.AddRange(memberRes);
            return baseRes;
        }
        
    }
}
