using PacMen.BL;
using PacMen.BL.Models;
using PacMen.PL.Data;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PacMen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly DbContextOptions<PacMenEntities> options;

        public UserController(ILogger<UserController> logger,
                                DbContextOptions<PacMenEntities> options)
        {
            this.options = options;
            this.logger = logger;
            logger.LogWarning("I was here!!!");
        }

        /// <summary>
        /// Return a list of users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return new UserManager(options).Load();
        }

        /// <summary>
        /// Get a particular user by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public User Get(Guid id)
        {
            return new UserManager(options).LoadById(id);
        }

        /// <summary>
        /// Insert a movie.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="rollback"></param>
        /// <returns>New Guid</returns>
        [HttpPost("{rollback?}")]
        public int Post([FromBody] User User, bool rollback = false)
        {
            try
            {
                return new UserManager(options).Insert(User, rollback);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update a movie.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="User"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        [HttpPut("{id}/{rollback?}")]
        public int Put(Guid id, [FromBody] User User, bool rollback = false)
        {
            try
            {
                return new UserManager(options).Update(User, rollback);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete a movie.
        /// </summary>
        /// <param name="id">Movie Id</param>
        /// <param name="rollback">Should be rollback the transacation</param>
        /// <returns></returns>
        [HttpDelete("{id}/{rollback?}")]
        public int Delete(Guid id, bool rollback = false)
        {
            try
            {
                return new UserManager(options).Delete(id, rollback);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
