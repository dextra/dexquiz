using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DexQuiz.Infrastructure.Persistence
{
    public class DexQuizContextFactory : IDesignTimeDbContextFactory<DexQuizContext>
    {
        public DexQuizContext CreateDbContext(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<DexQuizContext>();

            var connectionString = configuration.GetConnectionString("NpgDbConnection");

            builder.UseNpgsql(connectionString);

            return new DexQuizContext(builder.Options);
        }
    }
}