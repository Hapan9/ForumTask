using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTOs;
using DAL.Models;

namespace BLL.Mapers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDTO, User>()
                .ForMember("Password", uEt => uEt.MapFrom(uDTO => Hashing.GetHashString(uDTO.Password)))
                .ReverseMap();

            CreateMap<TopicDTO, Topic>()
                .ReverseMap();

            CreateMap<MessageDTO, Message>()
                .ReverseMap();
        }

        public static MapperConfiguration InitialazeAutoMapper()
        {
            var mapperConfiguration = new MapperConfiguration(conf => conf.AddProfile(new AutoMapperProfile()));

            return mapperConfiguration;
        }
    }
}
