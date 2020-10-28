using DexQuiz.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DexQuiz.Infrastructure.Persistence
{
    public class DexQuizContext : DbContext
    {
        public DexQuizContext(DbContextOptions<DexQuizContext> options) : base(options) { }

        public DbSet<Award> Awards { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnsweredQuestion> AnsweredQuestions { get; set; }
        public DbSet<AvailableQuestion> AvailableQuestions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<TrackRanking> TrackRankings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}