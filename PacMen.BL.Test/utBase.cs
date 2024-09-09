using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using PacMen.PL.Data;
using System.IO;

namespace PacMen.BL.Test
{
    
    [TestClass]
    public abstract class utBase
    {
        protected PacMenEntities dc;
        protected IDbContextTransaction transaction;
        private IConfigurationRoot _configuration;

        protected DbContextOptions<PacMenEntities> options;

        public utBase()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            options = new DbContextOptionsBuilder<PacMenEntities>()
                .UseSqlServer(_configuration.GetConnectionString("PacMenConnection"))
                .UseLazyLoadingProxies()
                .Options;

            dc = new PacMenEntities(options);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            dc = null;
        }
    }
}
