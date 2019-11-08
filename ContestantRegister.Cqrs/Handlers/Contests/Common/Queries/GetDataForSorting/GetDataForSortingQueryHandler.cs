using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContestantRegister.Cqrs.Features._Common.QueryHandlers;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Queries;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels.SelectedListItem;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.Extensions;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.QueryHandlers
{
    internal class GetDataForSortingQueryHandler : ReadRepositoryQueryHandler<GetDataForSortingQuery, DataForSorting>
    {
        private readonly IMapper _mapper;

        public GetDataForSortingQueryHandler(IReadRepository repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public override async Task<DataForSorting> HandleAsync(GetDataForSortingQuery query)
        {
            var compClassVMs = await ReadRepository.Set<CompClass>()
                .OrderBy(x => x.Name)
                .ProjectTo<CompClassSelectedListItemViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (query.SelectedCompClassIds != null)
            {
                foreach (var compClassVM in compClassVMs.Where(x => query.SelectedCompClassIds.Contains(x.Value)))
                {
                    compClassVM.Selected = true;
                }
            }

            var contestAreaVMs = await ReadRepository.Set<ContestArea>()
                //.Include(x => x.Area) // можно убрать, ведь достаем проекции, а не доменные объекты
                .Where(x => x.ContestId == query.ContestId)
                .OrderBy(x => x.Area.Name)
                .ProjectTo<ContestAreaSelectedListItemViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            if (query.SelectedContestAreaId.HasValue)
            {
                foreach (var contestAreaVM in contestAreaVMs.Where(x => x.Value == query.SelectedContestAreaId.Value))
                {
                    contestAreaVM.Selected = true;
                }
            }

            var result = new DataForSorting
            {
                ContestAreas = contestAreaVMs,
                CompClasses = compClassVMs
            };
            return result;
        }
    }
}
