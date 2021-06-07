﻿using DexQuiz.Core.Entities;
using DexQuiz.Core.Interfaces.Repositories;
using DexQuiz.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DexQuiz.Infrastructure.Repositories
{
    public class TrackRankingRepository : GenericRepository<TrackRanking>, ITrackRankingRepository
    {
        public TrackRankingRepository(DexQuizContext context) : base(context)
        {
        }
    }
}
