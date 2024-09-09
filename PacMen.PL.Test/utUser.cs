namespace PacMen.PL.Test
{
    [TestClass]
    public class utUser : utBase<tblUser>
    {

        [TestMethod]
        public void LoadTestSP()
        {
            var results = dc.Set<spGetUsers>().FromSqlRaw("exec spGetUsers").ToList();
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
            tblUser newRow = new tblUser();

            newRow.Id = Guid.NewGuid();
            newRow.FirstName = "XXXXXX";
            newRow.LastName = "XXXXXX";
            newRow.Email = "XXXXXX";
            newRow.UserName = "Test";
            newRow.Image = "Test";
            newRow.Password = "XXXXXX";
            newRow.ScoreId = base.LoadTest().FirstOrDefault().ScoreId;
            int rowsAffected = InsertTest(newRow);

            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblUser row = base.LoadTest().FirstOrDefault();

            if (row != null)
            {
                row.FirstName = "testts";
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblUser row = base.LoadTest().FirstOrDefault(x => x.FirstName == "Something");

            if (row != null)
            {
                int rowsAffected = DeleteTest(row);

                Assert.IsTrue(rowsAffected == 1);
            }


        }
    }
}
