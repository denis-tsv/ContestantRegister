using ContestantRegister.Cqrs.Features.Frontend.Contests.Individual.ViewModels;
using ContestantRegister.Framework.Cqrs;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Individual.Commands
{

    public abstract class CreateIndividualContestRegistrationCommand : ICommand
    {
        public CreateIndividualContestRegistrationViewModel ViewModel { get; set; }
    }
}
