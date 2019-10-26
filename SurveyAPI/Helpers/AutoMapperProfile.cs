using System;
using AutoMapper;
using SurveyAPI.Entities;

namespace SurveyAPI.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserBinder>();
            CreateMap<UserBinder, User>();
        }
    }
}
