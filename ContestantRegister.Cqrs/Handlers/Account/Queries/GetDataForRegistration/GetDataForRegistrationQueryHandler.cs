using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContestantRegister.Application.Handlers.Common.Handlers.Shared.ViewModels;
using ContestantRegister.Cqrs.Features._Common.QueryHandlers;
using ContestantRegister.Cqrs.Features.Frontend.Account.Queries;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.Extensions;

namespace ContestantRegister.Cqrs.Features.Frontend.Account.QueryHandlers
{
    internal class GetDataForRegistrationQueryHandler : ReadRepositoryQueryHandler<GetDataForRegistrationQuery, DataForRegistration>
    {
        private readonly IMapper _mapper;

        public GetDataForRegistrationQueryHandler(IReadRepository repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }
        public override async Task<DataForRegistration> HandleAsync(GetDataForRegistrationQuery query)
        {
            var result = new DataForRegistration();

            result.Cities = await ReadRepository.Set<City>()
                .OrderBy(c => c.Name)
                .ToListAsync();

            result.StudyPlaces = await ReadRepository.Set<StudyPlace>()
                .ProjectTo<StudyPlaceDropdownItemViewModel>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.ShortName)
                .ToListAsync();

            return result;
        }
    }
}
