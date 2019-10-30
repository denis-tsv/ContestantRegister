using AutoFilter;
using AutoFilter.Filters;
using AutoFilter.Filters.Convert;
using ContestantRegister.Cqrs.Features._Common.ListViewModel;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels;
using ContestantRegister.Framework.Cqrs;
using ContestantRegister.Models;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Queries
{
    public class ContestParticipantFilter
    {
        [NavigationProperty("Participant1", TargetPropertyName = "Surname")]
        public string ParticipantName { get; set; }

        [NavigationProperty("Trainer", TargetPropertyName = "Surname")]
        public string TrainerName { get; set; }

        [NavigationProperty("Manager", TargetPropertyName = "Surname")]
        public string ManagerName { get; set; }

        [NavigationProperty("StudyPlace.City", TargetPropertyName = "Name")]
        public string City { get; set; }

        [NavigationProperty("ContestArea.Area", TargetPropertyName = "Name")]
        public string Area { get; set; }

        [ConvertFilter(typeof(EnumDisplayToValueConverter<ContestRegistrationStatus>))]
        public string Status { get; set; }

        [NavigationProperty("StudyPlace", TargetPropertyName = "ShortName")]
        public string StudyPlace { get; set; }
    }

    public class GetContestDetailsQuery : IQuery<ContestInfoViewModelBase>
    {
        public ContestParticipantFilter Filter { get; set; }

        public int ContestId { get; set; }
    }
}
