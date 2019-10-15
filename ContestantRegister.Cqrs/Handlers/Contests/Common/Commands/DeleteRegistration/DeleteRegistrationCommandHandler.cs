using System.Threading.Tasks;
using ContestantRegister.Cqrs.Features._Common.CommandHandlers;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Commands;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.Exceptions;
using ContestantRegister.Services.Extensions;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.CommandHandlers
{
    internal class DeleteRegistrationCommandHandler : RepositoryCommandBaseHandler<DeleteRegistrationCommand>
    {
        public DeleteRegistrationCommandHandler(IRepository repository) : base(repository)
        {
        }

        public override async Task HandleAsync(DeleteRegistrationCommand command)
        {
            //TODO можно переделать на один sql запрос
            var registration = await Repository.Set<ContestRegistration>().SingleOrDefaultAsync(r => r.Id == command.RegistrationId);
            if (registration == null) throw new EntityNotFoundException();
            
            Repository.Remove(registration);
            await Repository.SaveChangesAsync();

            command.ContestId = registration.ContestId;
        }
    }
}
