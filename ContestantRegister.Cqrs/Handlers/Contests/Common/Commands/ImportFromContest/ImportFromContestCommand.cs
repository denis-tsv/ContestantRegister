using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels;
using ContestantRegister.Framework.Cqrs;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Commands
{
    public class ImportFromContestCommand : ICommand
    {
        public ImportContestParticipantsViewModel ViewModel { get; set; }
        public int ContestId { get; set; }
    }
}
