using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFilter;
using ContestantRegister.Cqrs.Features._Common.QueryHandlers;
using ContestantRegister.Cqrs.Features.Admin.Emails.Queries;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.Extensions;

namespace ContestantRegister.Cqrs.Features.Admin.Emails.QueryHandlers
{
    internal class GetEmailsQueryHandler : ReadRepositoryQueryHandler<GetEmailsQuery, List<Email>>
    {
        public GetEmailsQueryHandler(IReadRepository repository) : base(repository)
        {
        }

        public override async Task<List<Email>> HandleAsync(GetEmailsQuery query)
        {
            var emails = await ReadRepository.Set<Email>()
                .AutoFilter(query)
                .OrderByDescending(x => x.Id)
                .Take(100)
                .ToListAsync();

            return emails;
        }
    }
}
