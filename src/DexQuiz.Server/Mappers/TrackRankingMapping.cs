using AutoMapper;
using DexQuiz.Core.Entities;
using DexQuiz.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Server.Mappers
{
    public class TrackRankingMapping : Profile
    {
        public TrackRankingMapping()
        {
            CreateMap<TrackRanking, TrackRankingModel>().ReverseMap();
        }
    }
}
