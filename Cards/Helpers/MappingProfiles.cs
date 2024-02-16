using AutoMapper;
using Cards.DTOs.AccountsDtos;
using Cards.DTOs.CardDtos;
using Cards.DTOs.RolesDtos;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Cards.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Card, CardToEditDto>().ReverseMap();
            CreateMap<Card, CardToPatchDto>().ReverseMap();
            CreateMap<Card, CardToCreateDto>().ReverseMap();
            CreateMap<Card, CardToDisplayDto>().ReverseMap();

            CreateMap<IdentityRole, RoleDto>().ReverseMap();

            CreateMap<SystemUser, SystemUserToDisplayDto>().ReverseMap();
        }
    }
}