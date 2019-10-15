using AutoFilter;
using AutoFilter.Filters;
using ContestantRegister.Cqrs.Features._Common.Queries;
using ContestantRegister.Cqrs.Features.Admin.Schools.ViewModels;
using ContestantRegister.Models;

namespace ContestantRegister.Cqrs.Features.Admin.Schools.Queries
{
    public class GetMappedSchoolsQuery : GetMappedEntitiesQuery<School, SchoolListItemViewModel>
    {
        [StringFilter(StringFilterCondition.Contains, IgnoreCase = true)]
        public string ShortName { get; set; }

        [StringFilter(StringFilterCondition.Contains, IgnoreCase = true)]
        //[RelatedObject("City", PropertyName = "Name")]
        public string City { get; set; }
    }    
}
