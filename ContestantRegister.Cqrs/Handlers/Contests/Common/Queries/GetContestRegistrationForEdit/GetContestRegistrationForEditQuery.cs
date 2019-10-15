using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels;
using ContestantRegister.Framework.Cqrs;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Queries
{
    public class GetContestRegistrationForEditQuery : IQuery<ContestRegistrationViewModel>
    {
        public int RegistrationId { get; set; }
    }
}
