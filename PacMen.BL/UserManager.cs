

using PacMen.BL.Models;

namespace PacMen.BL
{
    public class LoginFailureException : Exception
    {
        public LoginFailureException() : base("Cannot log in with these credentials.")
        {
        }

        public LoginFailureException(string message) : base(message)
        {
        }
    }

    public class UserManager : GenericManager<tblUser>
    {
        public UserManager(DbContextOptions<PacMenEntities> options) : base(options) { }


        public static string GetHash(string Password)
        {
            using (var hasher = new System.Security.Cryptography.SHA1Managed())
            {
                var hashbytes = System.Text.Encoding.UTF8.GetBytes(Password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
            }
        }

        public void Seed()
        {
            List<User> users = Load();

            foreach (User user in users)
            {
                if (user.Password.Length != 28)
                {
                    Update(user);
                }
            }

            if (users.Count == 0)
            {
                // Hardcord a couple of users with hashed passwords
                Insert(new User { FirstName = "Admin", LastName = "Admin", Email = "user1.com", UserName = "Only", Image = "user.img", Password = "sdf" });
                Insert(new User { FirstName = "Admin1", LastName = "Admin1", Email = "user1.com", UserName = "Only2", Image = "user1.img", Password = "sd2f" });
            }
        }

        public bool Login(User user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.UserName))
                {
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        using (PacMenEntities dc = new PacMenEntities(options))
                        {
                            tblUser userrow = dc.tblUsers.FirstOrDefault(u => u.UserName == user.UserName);

                            if (userrow != null)
                            {
                                // check the password
                                if (userrow.Password == GetHash(user.Password))
                                {
                                    // Login was successfull
                                    user.Id = userrow.Id;
                                    user.FirstName = userrow.FirstName;
                                    user.LastName = userrow.LastName;
                                    user.Email = userrow.Email;
                                    user.UserName = userrow.UserName;
                                    user.Password = userrow.Password;
                                    user.Image = userrow.Image;
                                    user.ScoreId = userrow.ScoreId;
                                    return true;
                                }
                                else
                                {
                                    throw new LoginFailureException("Cannot log in with these credentials.");
                                }
                            }
                            else
                            {
                                throw new Exception("User could not be found.");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Password was not set.");
                    }
                }
                else
                {
                    throw new Exception("User Name was not set.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<User> Load()
        {
            try
            {
                List<User> Users = new List<User>();

                using (PacMenEntities dc = new PacMenEntities())
                {
                    Users = (from u in dc.tblUsers
                             join s in dc.tblScores on u.ScoreId equals s.Id
                             select new User
                             {
                                 Id = u.Id,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 UserName = u.UserName,
                                 Email = u.Email,
                                 Password = u.Password,
                                 Image = u.Image,
                                 ScoreId = u.ScoreId,
                                 Score = new Score
                                 {
                                     Id = s.Id,
                                     Scores = s.Score,
                                     Date = s.Date,
                                     Level = s.Level
                                 }
                             }).ToList();
                }
                return Users;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User LoadById(Guid id)
        {
            try
            {
                User user = new User();

                using (PacMenEntities dc = new PacMenEntities())
                {
                    user = (from u in dc.tblUsers
                            where u.Id == id
                            select new User
                            {
                                Id = u.Id,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                Email = u.Email,
                                UserName = u.UserName,
                                Password = u.Password,
                                Image = u.Image,
                                ScoreId = u.ScoreId
                            }).FirstOrDefault();
                }

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public int Insert(User user, bool rollback = false)
        //{
        //    try
        //    {
        //        int results = 0;
        //        using (PacMenEntities dc = new PacMenEntities(options))
        //        {
        //            IDbContextTransaction transaction = null;
        //            // Check if username already exists - do not allow ....
        //            bool inuse = dc.tblUsers.Any(u => u.UserName.Trim().ToUpper() == user.UserName.Trim().ToUpper());

        //            if (inuse && rollback == false)
        //            {
        //                throw new Exception("This User Name already exists.");
        //            }
        //            else
        //            {

        //                if (rollback) transaction = dc.Database.BeginTransaction();

        //                tblUser newUser = new tblUser();

        //                newUser.Id = Guid.NewGuid();
        //                newUser.FirstName = user.FirstName.Trim();
        //                newUser.LastName = user.LastName.Trim();
        //                newUser.Email = user.Email.Trim();
        //                newUser.UserName = user.UserName.Trim();
        //                newUser.Password = GetHash(user.Password.Trim());
        //                newUser.Image = user.Image.Trim();
        //                newUser.ScoreId = Guid.NewGuid();

        //                user.Id = newUser.Id;

        //                dc.tblUsers.Add(newUser);
        //                results = dc.SaveChanges();

        //                if (rollback) transaction.Rollback();
        //            }
        //        }
        //        return results;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public int Insert(User user, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (PacMenEntities dc = new PacMenEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblUser entity = new tblUser();

                    entity.Id = Guid.NewGuid();
                    entity.FirstName = user.FirstName;
                    entity.LastName = user.LastName;
                    entity.Email = user.Email;
                    entity.Image = user.Image;
                    entity.UserName = user.UserName;
                    entity.Password = GetHash(user.Password);
                    entity.ScoreId = user.ScoreId;

                    // IMPORTANT - BACK FILL THE ID
                    user.Id = entity.Id;

                    dc.tblUsers.Add(entity);
                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                }

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public int Update(User user, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (PacMenEntities dc = new PacMenEntities())
                {
                    // Check if username already exists - do not allow ....
                    tblUser existingUser = dc.tblUsers.Where(u => u.UserName.Trim().ToUpper() == user.UserName.Trim().ToUpper()).FirstOrDefault();

                    if (existingUser != null && existingUser.Id != user.Id && rollback == false)
                    {
                        throw new Exception("This User Name already exists.");
                    }
                    else
                    {
                        IDbContextTransaction transaction = null;
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        tblUser upDateRow = dc.tblUsers.FirstOrDefault(r => r.Id == user.Id);

                       // tblScore UpdateUserScore = dc.tblScores.FirstOrDefault(r => r.Id == user.ScoreId);

                        if (upDateRow != null )
                        {
                            upDateRow.FirstName = user.FirstName.Trim();
                            upDateRow.LastName = user.LastName.Trim();
                            upDateRow.Email = user.Email.Trim();
                            upDateRow.UserName = user.UserName.Trim();
                            upDateRow.Password = GetHash(user.Password.Trim());
                            upDateRow.Image = user.Image.Trim();
                            upDateRow.ScoreId = user.ScoreId;

                            dc.tblUsers.Update(upDateRow);

                            //dc.tblUsers.Update(UpdateUserScore);

                            // Commit the changes and get the number of rows affected
                            results = dc.SaveChanges();

                            if (rollback) transaction.Rollback();
                        }
                        else
                        {
                            throw new Exception("Row was not found.");
                        }
                    }
                }
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int Delete(Guid id, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (PacMenEntities dc = new PacMenEntities())
                {
                    // Check if user is associated with an exisiting order - do not allow delete ....
                    bool inuse = dc.tblScores.Any(o => o.Id == id);

                    if (inuse)
                    {
                        throw new Exception("This user is associated with an existing account and therefore cannot be deleted.");
                    }
                    else
                    {
                        IDbContextTransaction transaction = null;
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        tblUser deleteRow = dc.tblUsers.FirstOrDefault(r => r.Id == id);

                        if (deleteRow != null)
                        {
                            //Removes Users
                            dc.tblUsers.Remove(deleteRow);

                            // Commit the changes and get the number of rows affected
                            results = dc.SaveChanges();

                            if (rollback) transaction.Rollback();
                        }
                        else
                        {
                            throw new Exception("Row was not found.");
                        }
                    }
                }
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
