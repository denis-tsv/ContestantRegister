using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels;
using ContestantRegister.Framework.Cqrs;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Commands
{
    public class SortingCommand : ICommand
    {
        public int ContestId { get; set; }
        public SortingViewModel ViewModel { get; set; }
    }
}
