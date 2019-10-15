using System.Collections.Generic;
using AutoFilter;
using AutoFilter.Filters;
using AutoFilter.Filters.Convert;
using ContestantRegister.Framework.Cqrs;
using ContestantRegister.Models;

namespace ContestantRegister.Cqrs.Features.Admin.Emails.Queries
{
    public class GetEmailsQuery : IQuery<List<Email>>
    {
        [TargetPropertyName("Address", StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string Email { get; set; }

        [ConvertFilter(typeof(NullableIntToNullableBooleanConverter), TargetPropertyName = "IsSended")]
        public int? Sended { get; set; }

        [StringFilter(StringFilterCondition.Contains, IgnoreCase = true)]
        public string Message { get; set; }
    }
}
