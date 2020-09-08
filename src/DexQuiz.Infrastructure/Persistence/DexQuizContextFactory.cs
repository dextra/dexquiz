using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DexQuiz.Infrastructure.Persistence
{
    public class DexQuizContextFactory : IDesignTimeDbContextFactory<DexQuizContext>
    {
        public DexQuizContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                                              .SetBasePath(Directory.GetCurrentDirectory())
                                              .AddJsonFile("appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<DexQuizContext>();
            var connectionString = configuration.GetConnectionString("dbConnection");
            builder.UseSqlServer(connectionString);
            return new DexQuizContext(builder.Options);
        }
    }
}
