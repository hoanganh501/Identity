using AutoMapper;
using Domain.Entity;
using Identity.Contracts.Respsone;

namespace Application.Mapper
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<ApplicationUser, AuthResponse>();
        }
    }
}   
