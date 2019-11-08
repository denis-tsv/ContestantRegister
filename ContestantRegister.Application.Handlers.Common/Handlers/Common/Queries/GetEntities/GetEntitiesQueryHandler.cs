using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFilter;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContestantRegister.Cqrs.Features._Common.ListViewModel;
using ContestantRegister.Cqrs.Features._Common.Queries;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Framework.Extensions;
using ContestantRegister.Services.Extensions;

namespace ContestantRegister.Cqrs.Features._Common.QueryHandlers
{
    internal class GetEntitiesQueryHandler<TEntity, TViewModel> : ReadRepositoryQueryHandler<GetMappedEntitiesQuery<TEntity, TViewModel>, List<TViewModel>> where TEntity : class
    {
        private readonly IMapper _mapper;

        public GetEntitiesQueryHandler(IReadRepository repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public override async Task<List<TViewModel>> HandleAsync(GetMappedEntitiesQuery<TEntity, TViewModel> query)
        {
            var items = ReadRepository.Set<TEntity>()
                .ProjectTo<TViewModel>(_mapper.ConfigurationProvider)
                .AutoFilter(query);

            var orderBy = OrderByCache.GetOrderBy(typeof(TViewModel));

            if (!orderBy.IsEmpty())
            {
                items = items.OrderBy(orderBy);
            }
            
            var res = await items.ToListAsync();

            return res;
        }
    }
}
