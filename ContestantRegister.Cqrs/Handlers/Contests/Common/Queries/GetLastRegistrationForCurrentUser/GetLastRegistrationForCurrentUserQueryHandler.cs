using System.Linq;
using System.Threading.Tasks;
using ContestantRegister.Cqrs.Features._Common.QueryHandlers;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Queries;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.Extensions;
using ContestantRegister.Services.InfrastructureServices;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.QueryHandlers
{
    internal class GetLastRegistrationForCurrentUserQueryHandler : ReadRepositoryQueryHandler<GetLastRegistrationForCurrentUserQuery, ContestRegistration>
    {
        private readonly ICurrentUserService _currentUserService;

        public GetLastRegistrationForCurrentUserQueryHandler(IReadRepository repository, ICurrentUserService currentUserService) : base(repository)
        {
            _currentUserService = currentUserService;
        }

        public override async Task<ContestRegistration> HandleAsync(GetLastRegistrationForCurrentUserQuery query)
        {
            var registration = await ReadRepository.Set<ContestRegistration>()
                .Include(r => r.Contest)
                .Where(x => x.RegistredBy.Id == _currentUserService.Id && x.RegistrationDateTime.HasValue)
                .OrderByDescending(x => x.RegistrationDateTime)
                .FirstOrDefaultAsync();

            return registration;
        }
    }
}
