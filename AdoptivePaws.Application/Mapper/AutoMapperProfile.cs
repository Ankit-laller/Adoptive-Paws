using AdoptivePaws.Core.Dtos.User;
using AdoptivePaws.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserEntity, UserDto>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SNo))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.PhoneNo, opt => opt.MapFrom(src => src.PhoneNo))
           .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City));
        }
    }
}
