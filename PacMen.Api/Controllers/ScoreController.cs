using PacMen.BL;
using PacMen.BL.Models;
using PacMen.PL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.SqlServer.Server;

namespace PacMen.API.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : GenericController<Score, ScoreManager>
    {
        public ScoreController(ILogger<ScoreController> logger,
                                DbContextOptions<PacMenEntities> options) : base(logger, options)
        {
        }

    }
}
