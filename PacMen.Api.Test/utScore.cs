using PacMen.BL.Models;

namespace PacMen.Api.Test
{
    [TestClass]
    public class utScore : utBase
    {
        [TestMethod]
        public async Task LoadTestAsync()
        {
            await base.LoadTestAsync<Score>();
        }

        [TestMethod]
        public async Task InsertTestAsync()
        {
            Score score = new Score { Level = 3 };
            await base.InsertTestAsync<Score>(score);

        }

        [TestMethod]
        public async Task DeleteTestAsync()
        {
            await base.DeleteTestAsync1<Score>(new KeyValuePair<string, string>("Level", "3"));
        }

        [TestMethod]
        public async Task LoadByIdTestAsync()
        {
            await base.LoadByIdTestAsync<Score>(new KeyValuePair<string, string>("Level", "3"));
        }

        [TestMethod]
        public async Task UpdateTestAsync()
        {
            Score score = new Score
            {
                Scores = 3,
                Date = DateTime.Now,
                Level = 4,
            };
            await base.UpdateTestAsync<Score>(new KeyValuePair<string, string>("Level", "3"), score);

        }

    }
}
