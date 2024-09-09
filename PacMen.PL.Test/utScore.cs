namespace PacMen.PL.Test
{
    [TestClass]
    public class utScore : utBase<tblScore>
    {

        [TestMethod]
        public void LoadTestSP()
        {
            var results = dc.Set<spGetScores>().FromSqlRaw("exec spGetScores").ToList();
            Assert.AreEqual(3, results.Count);

        }

        [TestMethod]
        public void LoadTest()
        {
            int expected = 3;
            var companys = base.LoadTest();
            Assert.AreEqual(expected, companys.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblScore newRow = new tblScore();

            newRow.Id = Guid.NewGuid();
           // newRow.UserId = base.LoadTest().FirstOrDefault().UserId;
            newRow.Score = 5;
            newRow.Date = DateTime.Now;
            newRow.Level = 5;
            int rowsAffected = InsertTest(newRow);

            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblScore row = base.LoadTest().FirstOrDefault();

            if (row != null)
            {
                row.Score = 2;
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblScore row = base.LoadTest().FirstOrDefault(x => x.Score == 3);

            if (row != null)
            {
                int rowsAffected = DeleteTest(row);

                Assert.IsTrue(rowsAffected == 1);
            }


        }
    }
}
