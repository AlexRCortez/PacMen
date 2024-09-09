using Microsoft.AspNetCore.Mvc;
using PacMen.Api.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using PacMen.BL;
using PacMen.BL.Models;
using PacMen.PL.Data;

namespace PacMen.API.Controllers
{
    /// <summary>D
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class GenericController<T, U> : ControllerBase
    {
        protected DbContextOptions<PacMenEntities> options;
        protected readonly ILogger logger;
        dynamic manager;

        public GenericController(ILogger logger,
                                 DbContextOptions<PacMenEntities> options)
        {
            this.options = options;
            this.logger = logger;
            manager = (U)Activator.CreateInstance(typeof(U), logger, options);
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> Get()
        {
            try
            {
                return Ok(await manager.LoadAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Get(Guid id)
        {
            try
            {
                return Ok(await manager.LoadByIdAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("{rollback?}")]
        public async Task<ActionResult> Post([FromBody] T entity, bool rollback = false)
        {
            try
            {
                Guid id = await manager.InsertAsync(entity, rollback);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }





        [HttpPut("{id}/{rollback?}")]
        public int Put(Guid id, [FromBody] Score score, bool rollback = false)
        {
            try
            {
                return new ScoreManager(options).Update(score, rollback);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}/{rollback?}")]
        public int Delete(Guid id, bool rollback = false)
        {
            try
            {
                return new ScoreManager(options).Delete(id, rollback);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
