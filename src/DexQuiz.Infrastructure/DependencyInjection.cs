using DexQuiz.Core.Interfaces.Repositories;
using DexQuiz.Core.Interfaces.Services;
using DexQuiz.Core.Interfaces.UoW;
using DexQuiz.Core.Services;
using DexQuiz.Infrastructure.Persistence;
using DexQuiz.Infrastructure.Repositories;
using DexQuiz.Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DexQuiz.Infrastructure
{
    public static class DependencyInjection
    {
        public static IConfiguration Configuration { get; set; }

        public static void ConfigureDependencyInjectionForApi(this IServiceCollection services, IConfiguration configuration)
        {
            Configuration = configuration;

            services.AddDbContext<DexQuizContext>(options => options.UseLazyLoadingProxies().UseNpgsql(Configuration.GetConnectionString("NpgDbConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAnsweredQuestionRepository, AnsweredQuestionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IAvailableQuestionRepository, AvailableQuestionRepository>();
            services.AddScoped<IAwardRepository, AwardRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ITrackRankingRepository, TrackRankingRepository>();
            services.AddScoped<ITrackRepository, TrackRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<IRankingService, RankingService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        public static void ConfigureDependencyInjectionForUnitTests(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAnsweredQuestionRepository, AnsweredQuestionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IAvailableQuestionRepository, AvailableQuestionRepository>();
            services.AddScoped<IAwardRepository, AwardRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ITrackRankingRepository, TrackRankingRepository>();
            services.AddScoped<ITrackRepository, TrackRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<IRankingService, RankingService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        public static void MigrateDatabase(this IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider.CreateScope();

            try
            {
                var context = serviceScope.ServiceProvider.GetService<DexQuizContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = serviceScope.ServiceProvider.GetService<ILogger>();
                logger.LogError(ex, "An error occurred migrating database.");
            }
        }
    }
}
