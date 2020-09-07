using AutoMapper;
using DexQuiz.Core.Entities;
using DexQuiz.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Server.Mappers
{
    public class QuestionMapping : Profile
    {
        public QuestionMapping()
        {
            CreateMap<Question, QuestionModel>().ReverseMap();
            CreateMap<Answer, AnswerModel>().ReverseMap();
            CreateMap<Question, QuestionForUserModel>().ReverseMap();
            CreateMap<Answer, AnswerForUserModel>().ReverseMap();
            CreateMap<AnsweredQuestion, AnsweredQuestionModel>().ReverseMap();
        }
    }
}
