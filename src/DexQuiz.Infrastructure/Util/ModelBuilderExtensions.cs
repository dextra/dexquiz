using DexQuiz.Core.Entities;
using DexQuiz.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Infrastructure.Util
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var award = new Award { Id = 1, Description = "Fone gamer", Position = 1, Type = AwardType.Track };

            var devrootsTrack = new { Id = 1, Name = "Devroots", AwardId = 1 };
            var dataTrack = new { Id = 2, Name = "Data Track", AwardId = 1 };
            var alemDoCodigoTrack = new { Id = 3, Name = "Além do código", AwardId = 1 };
            var cxTrack = new { Id = 4, Name = "CX", AwardId = 1 };
            var nodeTrack = new { Id = 5, Name = "Node", AwardId = 1 };
            var dotnet = new { Id = 6, Name = ".Net", AwardId = 1 };
            var frontendTrack = new { Id = 7, Name = "Frontend", AwardId = 1 };
            var androidTrack = new { Id = 8, Name = "Android", AwardId = 1 };
            var iosTrack = new { Id = 9, Name = "iOS", AwardId = 1 };
            var javaTrack = new { Id = 10, Name = "Java", AwardId = 1 };

            modelBuilder.Entity<Award>().HasData(award);
            modelBuilder.Entity<Track>().HasData(devrootsTrack, dataTrack, alemDoCodigoTrack, cxTrack, nodeTrack, dotnet, frontendTrack, androidTrack, iosTrack, javaTrack);
        }
    }
}
