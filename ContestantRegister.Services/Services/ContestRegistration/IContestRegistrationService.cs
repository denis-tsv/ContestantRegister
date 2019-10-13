using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContestantRegister.Services.DomainServices.ContestRegistration
{
    public interface IContestRegistrationService
    {
        Task<List<KeyValuePair<string, string>>> ValidateCreateIndividualContestRegistrationAsync(ICreateIndividualContestRegistration viewModel);
        Task<List<KeyValuePair<string, string>>> ValidateEditIndividualContestRegistrationAsync(IEditIndividualContestRegistration viewModel);
        List<KeyValuePair<string, string>> ValidateIndividualContestMember(IIndividualContestRegistration viewModel);

        Task<List<KeyValuePair<string, string>>> ValidateCreateTeamContestRegistrationAsync(ICreateTeamContestRegistration viewModel);
        Task<List<KeyValuePair<string, string>>> ValidateEditTeamContestRegistrationAsync(IEditTeamContestRegistration viewModel);
        List<KeyValuePair<string, string>> ValidateTeamContestRegistrationMember(ITeamContestRegistration viewModel);
    }
}
