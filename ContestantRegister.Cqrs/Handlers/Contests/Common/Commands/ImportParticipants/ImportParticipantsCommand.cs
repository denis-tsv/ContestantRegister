using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels;
using ContestantRegister.Framework.Cqrs;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Commands
{
    public class ImportParticipantsCommand : ICommand
    {
        public int ContestId { get; set; }
        public ImportParticipantsViewModel ViewModel { get; set; }
    }
}
