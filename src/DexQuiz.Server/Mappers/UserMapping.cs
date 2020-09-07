using AutoMapper;
using DexQuiz.Core.Entities;
using DexQuiz.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Server.Mappers
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserModel, User>().ReverseMap();
        }
    }
}
