using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PacMen.PL.Test
{
    [TestClass]
    public class utBase<T> where T : class
    {
        protected PacMenEntities dc;  // declare the DataContext
        protected IDbContextTransaction transaction;
        private IConfigurationRoot _configuration;
        private DbContextOptions<PacMenEntities> options;

        public utBase()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
            options = new DbContextOptionsBuilder<PacMenEntities>()
                .UseSqlServer(_configuration.GetConnectionString("DatabaseConnection"))
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

        public List<T> LoadTest()
        {
            return dc.Set<T>().ToList();
        }

        public int InsertTest(T row)
        {
            dc.Set<T>().Add(row);
            return dc.SaveChanges();
        }

        public int UpdateTest(T row)
        {

            dc.Entry(row).State = EntityState.Modified;
            return dc.SaveChanges();
        }

        public int DeleteTest(T row)
        {
            dc.Set<T>().Remove(row);
            return dc.SaveChanges();
        }
    }
}
