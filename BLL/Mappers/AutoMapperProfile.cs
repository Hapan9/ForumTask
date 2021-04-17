using AutoMapper;
using BLL.DTOs;
using DAL.Models;

namespace BLL.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember("Password", uEt => uEt.MapFrom(uDto => Hashing.GetHashString(uDto.Password)))
                .ReverseMap();

            CreateMap<TopicDto, Topic>()
                .ReverseMap();

            CreateMap<MessageDto, Message>()
                .ReverseMap();
        }

        public static MapperConfiguration InitializeAutoMapper()
        {
            var mapperConfiguration = new MapperConfiguration(conf => conf.AddProfile(new AutoMapperProfile()));

            return mapperConfiguration;
        }
    }
}