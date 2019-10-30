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
        public string Email { get; set; }

        [ConvertFilter(typeof(NullableIntToNullableBooleanConverter))]
        public int? EmailConfirmed { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string StudyPlace { get; set; }

        [ConvertFilter(typeof(EnumDisplayToValueConverter<UserType>), TargetPropertyName = nameof(ApplicationUser.UserType))]
        public string UserTypeName { get; set; }
    }
}
