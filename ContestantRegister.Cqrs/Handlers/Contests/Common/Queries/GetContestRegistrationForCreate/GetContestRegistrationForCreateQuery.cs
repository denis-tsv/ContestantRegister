using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels;
using ContestantRegister.Framework.Cqrs;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Queries
{
    public class GetContestRegistrationForCreateQuery : IQuery<ContestRegistrationViewModel>
    {
        public int ContestId { get; set; }
    }
}
