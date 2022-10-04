using AutoMapper;
using DataAccess.Models;
using DataAccess.ViewModels;

namespace BezahlsystemAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserForRegistrationDto>();
            CreateMap<UserForRegistrationDto, ApplicationUser>();
        }
    }
}
