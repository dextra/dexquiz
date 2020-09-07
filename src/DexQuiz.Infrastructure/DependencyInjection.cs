using DexQuiz.Core.Interfaces.Services;
using DexQuiz.Core.Interfaces.UoW;
using DexQuiz.Core.Services;
using DexQuiz.Infrastructure.Persistence;
using DexQuiz.Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Infrastructure
{
    public static class DependencyInjection
    {
        public static IConfiguration Configuration { get; set; }

        public static void ConfigureDependencyInjectionForApi(this IServiceCollection services, IConfiguration configuration)
        {
            Configuration = configuration;
            services.AddDbContext<DexQuizContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("dbConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<IRankingService, RankingService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        public static void MigrateDatabase(this IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<DexQuizContext>();
            context.Database.Migrate();
        }
    }
}
