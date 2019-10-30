using AutoFilter;
using AutoFilter.Filters;
using ContestantRegister.Cqrs.Features._Common.Queries;
using ContestantRegister.Cqrs.Features.Admin.Schools.ViewModels;
using ContestantRegister.Models;

namespace ContestantRegister.Cqrs.Features.Admin.Schools.Queries
{
    public class GetMappedSchoolsQuery : GetMappedEntitiesQuery<School, SchoolListItemViewModel>
    {
        public string ShortName { get; set; }

        public string City { get; set; }
    }    
}
