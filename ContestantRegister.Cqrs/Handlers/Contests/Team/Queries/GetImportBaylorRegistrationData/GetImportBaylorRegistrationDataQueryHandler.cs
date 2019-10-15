using System.Linq;
using System.Threading.Tasks;
using ContestantRegister.Cqrs.Features._Common.QueryHandlers;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Team.Queries;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.Exceptions;
using ContestantRegister.Services.Extensions;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Team.QueryHandlers
{
    internal class GetImportBaylorRegistrationDataQueryHandler : ReadRepositoryQueryHandler<GetImportBaylorRegistrationDataQuery, ImportParticipantsViewModel>
    {
        public GetImportBaylorRegistrationDataQueryHandler(IReadRepository repository) : base(repository)
        {
        }

        public override async Task<ImportParticipantsViewModel> HandleAsync(GetImportBaylorRegistrationDataQuery query)
        {
            var contestName = await ReadRepository.Set<Contest>()
                .Where(c => c.Id == query.ContestId)
                .Select(x => x.Name)
                .SingleOrDefaultAsync();
            if (contestName == null) throw new EntityNotFoundException();
            
            return new ImportParticipantsViewModel
            {
                ContestName = contestName
            };
        }
    }
}
