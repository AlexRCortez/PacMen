using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.SqlServer.Server;
using PacMen.BL.Models;
using PacMen.PL.Entities;
using PacMen.Reporting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PacMen.BL.Test
{
    [TestClass]
    public class utScore : utBase
    {

        //[TestMethod]
        //public void LoadTestSP()
        //{
        //    int expected = 1;

        //    var parameter1 = new SqlParameter
        //    {
        //        ParameterName = "Level",
        //        SqlDbType = System.Data.SqlDbType.Int,
        //        Value = 3
        //    };

        //   List<spGetScores> results= dc.Set<spGetScores>().FromSqlRaw("exec spGetScores @Level", parameter1).ToList();

        //    int score = results[1].Score;
        //    foreach (var r in results)
        //    {
        //        score = r.Score;
        //    }

        //    Assert.AreEqual(50, score);
        //    Assert.AreEqual(expected, results.Count);
        //}


        [TestMethod]
        public async Task LoadTestAsync()
        {
            List<Score> score = await new ScoreManager(options).LoadAsync();
            int expected = 3;
            Assert.AreEqual(expected, score.Count);
        }



        [TestMethod]
        public async Task InsertTestAsyncFail()
        {
            try
            {
                Score score = new Score
                {
                    Date = DateTime.Now,
                    Scores = 11,
                    Level = 5
                };

                Guid result = await new ScoreManager(options).InsertAsync(score, true);
                Assert.AreNotEqual(result, Guid.Empty);
            }
            catch (AlreadyExistsException ex)
            {
                Assert.IsTrue(true);
            }

            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task InsertTestAsync()
        {
            Score score = new Score
            {
                Date = DateTime.Now,
                Scores = 2,
                Level = 10
            };

            Guid result = await new ScoreManager(options).InsertAsync(score, true);
            Assert.AreNotEqual(result, Guid.Empty);
        }





        [TestMethod]
        public void utReportTest()
        {
            var scores = new ScoreManager(options).Load();
            string[] columns = { "Scores", "Date", "Level" };
            var data = ScoreManager.ConvertData<Score>(scores, columns);
            Excel.Export("pacmenScores.xlsx", data);
        }

        [TestMethod]
        public void LoadTest() // Push test
        {
            List<Score> scores = new ScoreManager(options).Load();
            int expected = 3;

            Assert.AreEqual(expected, scores.Count);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            Guid id = new ScoreManager(options).Load().FirstOrDefault().Id;
            Assert.AreEqual(new ScoreManager(options).LoadById(id).Id, id);
        }

        //[TestMethod]
        //public void LoadByOrderIdTest()
        //{
        //    Guid orderId = new ScoreManager(options).Load().FirstOrDefault().Id;
        //    Assert.IsTrue(new ScoreManager(options).LoadByOrderId(orderId).Count > 0);
        //}


        [TestMethod]
        public void InsertTest()
        {
            Score score = new Score
            {
                Scores = 44,
                Date = DateTime.Now,
                Level = 3
            };

            int result = new ScoreManager(options).Insert(score, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Score score = new ScoreManager(options).Load().FirstOrDefault();
            score.Level = 300;

            Assert.IsTrue(new ScoreManager(options).Update(score, true) > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Score score = new ScoreManager(options).Load().FirstOrDefault();
            Assert.IsTrue(new ScoreManager(options).Delete(score.Id, true) > 0);
        }
    }
}
