using AutoMapper;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels.SelectedListItem;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Team.ViewModels;
using ContestantRegister.Cqrs.Features.Frontend.Home.ViewModels;
using ContestantRegister.Models;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Utils
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<ContestRegistrationViewModel, IndividualContestRegistration>()
                .ReverseMap();
            
            CreateMap<TeamContestRegistrationViewModel, TeamContestRegistration>()
                .ReverseMap();            
            
            CreateMap<Contest, SortingViewModel>();

            CreateMap<CompClass, CompClassSelectedListItemViewModel>()
                .ForMember(vm => vm.Text, opt => opt.MapFrom(ca => ca.Name))
                .ForMember(vm => vm.Value, opt => opt.MapFrom(ca => ca.Id));

            CreateMap<ContestArea, ContestAreaSelectedListItemViewModel>()
                .ForMember(vm => vm.Text, opt => opt.MapFrom(ca => ca.Area.Name))
                .ForMember(vm => vm.Value, opt => opt.MapFrom(ca => ca.Id));

            CreateMap<ApplicationUser, UserForContestRegistrationListItemViewModel>()
                .ForMember(ulivm => ulivm.DisplayName, opt => opt.MapFrom(au => $"{au.Name} {au.Surname} ({au.Email})"));

            CreateMap<ContestRegistrationDto, IndividualContestRegistration>()
                .ForMember(e => e.Status, opt => opt.Ignore())
                .ForMember(e => e.Number, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ContestRegistrationDto, TeamContestRegistration>()
                .ForMember(e => e.Status, opt => opt.Ignore())
                .ForMember(e => e.Number, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
