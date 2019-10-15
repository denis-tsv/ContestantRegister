using AutoFilter;
using AutoFilter.Filters;
using AutoFilter.Filters.Convert;
using ContestantRegister.Cqrs.Features._Common.ListViewModel;
using ContestantRegister.Cqrs.Features._Common.Queries;
using ContestantRegister.Cqrs.Features.Admin.Users.ViewModels;
using ContestantRegister.Models;

namespace ContestantRegister.Cqrs.Features.Admin.Users.Queries
{
    public class GetUsersQuery : GetMappedEntitiesQuery<ApplicationUser, UserListItemViewModel>
    {
        [StringFilter(StringFilterCondition.Contains, IgnoreCase = true)]
        public string Email { get; set; }

        [ConvertFilter(typeof(NullableIntToNullableBooleanConverter))]
        public int? EmailConfirmed { get; set; }

        [StringFilter(StringFilterCondition.Contains, IgnoreCase = true)]
        public string Surname { get; set; }

        [StringFilter(StringFilterCondition.Contains, IgnoreCase = true)]
        public string Name { get; set; }

        [StringFilter(StringFilterCondition.Contains, IgnoreCase = true)]
        public string City { get; set; }

        [StringFilter(StringFilterCondition.Contains, IgnoreCase = true)]
        public string StudyPlace { get; set; }

        [ConvertFilter(typeof(EnumDisplayToValueConverter<UserType>))]
        [TargetPropertyNameAttribute("UserType")]
        public string UserTypeName { get; set; }
    }
}
