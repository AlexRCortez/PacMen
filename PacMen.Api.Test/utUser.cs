using PacMen.BL.Models;

namespace PacMen.Api.Test
{
    [TestClass]
    public class utUser : utBase
    {
        [TestMethod]
        public async Task LoadTestAsync()
        {
            await base.LoadTestAsync<User>();
        }

        [TestMethod]
        public async Task InsertTestAsync()
        {
            User user = new User { UserName = "Test" };
            await base.InsertTestAsync<User>(user);

        }

        [TestMethod]
        public async Task DeleteTestAsync()
        {
            await base.DeleteTestAsync1<User>(new KeyValuePair<string, string>("UserName", "arosas"));
        }

        [TestMethod]
        public async Task LoadByIdTestAsync()
        {
            await base.LoadByIdTestAsync<User>(new KeyValuePair<string, string>("UserName", "arosas"));
        }

        [TestMethod]
        public async Task UpdateTestAsync()
        {
            User user = new User
            {

                FirstName = "Test",
                LastName = "Test",
                Email = "test@example.com",
                UserName = "Test",
                Image = "test.jpg",
                Password = "password",
                ScoreId = Guid.NewGuid()
            };
            await base.UpdateTestAsync<User>(new KeyValuePair<string, string>("FirstName", "Test"), user);

        }

    }
}
