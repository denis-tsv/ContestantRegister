using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using ContestantRegister.Application.Handlers.Common.Handlers.Shared.ViewModels;
using ContestantRegister.Cqrs.Features._Common.QueryHandlers;
using ContestantRegister.Cqrs.Features.Frontend.Manage.Queries;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.Extensions;

namespace ContestantRegister.Cqrs.Features.Frontend.Manage.QueryHandlers
{
    internal class GetDataForProfileQueryHandler : ReadRepositoryQueryHandler<GetDataForProfileQuery, DataForProfile>
    {
        public GetDataForProfileQueryHandler(IReadRepository repository) : base(repository)
        {
        }

        public override async Task<DataForProfile> HandleAsync(GetDataForProfileQuery query)
        {
            var result = new DataForProfile();

            result.Cities = await ReadRepository.Set<City>()
                .OrderBy(x => x.Name)
                .ToListAsync();

            result.StudyPlaces = await ReadRepository.Set<StudyPlace>()
                .ProjectTo<StudyPlaceDropdownItemViewModel>()
                .OrderBy(x => x.ShortName)
                .ToListAsync();

            return result;
        }
    }
}
