using AutoMapper;
using ContestantRegister.Cqrs.Features.Frontend.Account.ViewModels;
using ContestantRegister.Models;

namespace ContestantRegister.Cqrs.Features.Frontend.Account.Utils
{
    public class AccountAutoMapperProfile : Profile
    {
        public AccountAutoMapperProfile()
        {
            CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(x => x.Surname, opt => opt.MapFrom(y => y.Surname.Trim()))
                .ForMember(x => x.Name, opt => opt.MapFrom(y => y.Name.Trim()))
                .ForMember(x => x.Patronymic, opt => opt.MapFrom(y => y.Patronymic.Trim()))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(y => y.FirstName.Trim()))
                .ForMember(x => x.LastName, opt => opt.MapFrom(y => y.LastName.Trim()))
                ;
        }
    }
}

