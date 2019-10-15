using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFilter;
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
        public GetEntitiesQueryHandler(IReadRepository repository) : base(repository)
        {
        }

        public override async Task<List<TViewModel>> HandleAsync(GetMappedEntitiesQuery<TEntity, TViewModel> query)
        {
            var items = ReadRepository.Set<TEntity>()
                .ProjectTo<TViewModel>()
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
