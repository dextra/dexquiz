using AutoMapper;
using DexQuiz.Core.Entities;
using DexQuiz.Server.Models;

namespace DexQuiz.Server.Mappers
{
    public class TrackWithRankingMapping : Profile
    {
        public TrackWithRankingMapping()
        {
            CreateMap<TrackWithRanking, TrackWithRankingModel>().ReverseMap();
        }
    }
}