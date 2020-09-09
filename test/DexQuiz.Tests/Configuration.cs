using AutoMapper;

using DexQuiz.Infrastructure;
using DexQuiz.Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DexQuiz.Tests
{
    [TestFixture]
    public class Configuration
    {
        protected ServiceProvider ServiceProvider { get; set; }
        private SqliteConnection _connection;

        protected void CreateNewDatabaseInMemory()
        {
            var context = ServiceProvider.GetService<DexQuizContext>();
            context.Database.EnsureCreated();
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            var dbConnection = "datasource=:memory;mode=memory;cache=shared";
            _connection = new SqliteConnection(dbConnection);
            _connection.Open();

            var services = new ServiceCollection();
            services.ConfigureDependencyInjectionForUnitTests();
            services.AddDbContext<DexQuizContext>(options =>
                                            options.UseLazyLoadingProxies()
                                            .UseSqlite(dbConnection).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                                            , ServiceLifetime.Scoped, ServiceLifetime.Scoped);
            services.AddAutoMapper(GetOnlySolutionAssemblies());

            ServiceProvider = services.BuildServiceProvider();
        }


        [OneTimeTearDown]
        public void TearDown()
        {
            _connection.Close();
        }

        private static IList<Assembly> GetOnlySolutionAssemblies()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => (a.FullName.StartsWith("DexQuiz.")))
                .OrderByDescending(x => x.FullName)
                .ToList();
        }
    }
}
