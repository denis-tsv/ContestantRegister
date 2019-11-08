using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContestantRegister.Application.Handlers.Common.Handlers.Shared.ViewModels;
using ContestantRegister.Cqrs.Features._Common.QueryHandlers;
using ContestantRegister.Cqrs.Features.Frontend.Home.Queries;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.Extensions;

namespace ContestantRegister.Cqrs.Features.Frontend.Home.QueryHandlers
{
    internal class GetRegisterParticipantDataQueryHandler : ReadRepositoryQueryHandler<GetRegisterParticipantDataQuery, RegisterParticipantData>
    {
        private readonly IMapper _mapper;

        public GetRegisterParticipantDataQueryHandler(IReadRepository repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }
        public override async Task<RegisterParticipantData> HandleAsync(GetRegisterParticipantDataQuery query)
        {
            var cities = await ReadRepository.Set<City>()
                .OrderBy(x => x.Name)
                .ToListAsync();

            var studyPlaces = await ReadRepository.Set<StudyPlace>()
                .ProjectTo<StudyPlaceDropdownItemViewModel>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.ShortName)
                .ToListAsync();

            return new RegisterParticipantData
            {
                Cities = cities,
                StudyPlaces = studyPlaces,
            };
        }
    }
}
