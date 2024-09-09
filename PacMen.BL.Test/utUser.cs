using PacMen.BL.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using iText.Forms.Xfdf;
using PacMen.Reporting;

namespace PacMen.BL.Test
{
    [TestClass]
    public class utUser : utBase
    {

        [TestInitialize]
        public void Initialize()
        {
            new UserManager(options).Seed();
        }


        [TestMethod]
        public void utReportTest()
        {
            var users = new UserManager(options).Load();
            string[] columns = { "FirstName", "LastName", "Email", "UserName", "Image", "Password"};
            var data = UserManager.ConvertData<User>(users, columns);
            Excel.Export("pacmenUsers.xlsx", data);
        }

        [TestMethod]
        public void LoadTest()
        {
            List<User> users = new UserManager(options).Load();
            Assert.IsTrue(users.Count > 0);
        }

        [TestMethod]
        public void InsertTest()
        {
            User user = new User { FirstName = "Admign", LastName = "Admin",Email = "user1.com", UserName = "Only", Image ="user.img", Password = "sdf", ScoreId = new ScoreManager(options).Load().FirstOrDefault().Id, };
            int result = new UserManager(options).Insert(user, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void LoginSuccess()
        {
            User user = new User { FirstName = "Alex", LastName = "Rosas", Email = "arosas@gmail.com", UserName = "arosas", Image = "arosas", Password = "rosas123" };
            bool result = new UserManager(options).Login(user);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LoginFail()
        {
            try
            {
                User user = new User { FirstName = "Alex", LastName = "Rosas", Email = "arosas@gmail.com", UserName = "arosas", Image = "arosas", Password = "xxxx" };
                new UserManager(options).Login(user);
                Assert.Fail();
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            User user = new UserManager(options).Load().FirstOrDefault();
            user.UserName = "something";

            Assert.IsTrue(new UserManager(options).Update(user, true) > 0);
        }



    }
}