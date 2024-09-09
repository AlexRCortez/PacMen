using Castle.Core.Resource;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Server;
using PacMen.BL.Models;
using PacMen.PL.Data;
using PacMen.PL.Entities;
using System.IO;
using System.Threading.Tasks;

namespace PacMen.BL
{
    public class ScoreManager : GenericManager<tblScore>
    {
        public ScoreManager(DbContextOptions<PacMenEntities> options) : base(options)
        {

        }
        public ScoreManager(ILogger logger, DbContextOptions<PacMenEntities> options) : base(logger, options) { }
        public int Insert(Score score, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (PacMenEntities dc = new PacMenEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    tblScore row = new tblScore();

                    row.Id = Guid.NewGuid();
                    row.Score = score.Scores;
                    row.Date = DateTime.Now;
                    row.Level = score.Level;

                    score.Id = row.Id;

                    dc.tblScores.Add(row);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();
                }

                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<Guid> InsertAsync(Score score, bool rollback = false)
        {
            try
            {
                tblScore row = new tblScore { Score = score.Scores, Level = score.Level, Date = score.Date };
                Guid id = await InsertAsync(row, e => e.Level == score.Level, rollback);
                score.Id = id;
                return id;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Score>> LoadAsync()
        {

            try
            {
                List<Score> rows = new List<Score>();
                (await base.LoadAsync())
                    .ToList()
                    .ForEach(d => rows.Add(
                        new Score
                        {
                            Id = d.Id,
                            Scores = d.Score,
                            Date = d.Date,
                            Level = d.Level,
                        }));

                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }

            public int Update(Score score, bool rollback = false)
        {
            try
            {
                int results;
                using (PacMenEntities dc = new PacMenEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblScore row = dc.tblScores.FirstOrDefault(s => s.Id == score.Id);

                    if (row != null)
                    {
                        row.Score = score.Scores;
                        row.Date = DateTime.Now;
                        row.Level = score.Level;

                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Delete(Guid id, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (PacMenEntities dc = new PacMenEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Get the row that we are trying to update
                    tblScore deleteRow = dc.tblScores.FirstOrDefault(g => g.Id == id);

                    if (deleteRow != null)
                    {

                        //Delets User Scores
                        var userScores = dc.tblUsers.Where(i => i.ScoreId == id);
                        dc.tblUsers.RemoveRange(userScores);

                        //DeletsScores
                        dc.tblScores.Remove(deleteRow);
                        results = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }

                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Score> Load()
        {
            try
            {
                List<Score> rows = new List<Score>();
                base.Load()
                    .ForEach(d => rows.Add(
                        new Score
                        {
                            Id = d.Id,
                            Scores = d.Score,
                            Level = d.Level,
                            Date = d.Date,  
                        }));

                return rows;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public Score LoadById(Guid id)
        {
            try
            {
                using (PacMenEntities dc = new PacMenEntities(options))
                {
                    tblScore row = dc.tblScores.FirstOrDefault(s => s.Id == id);

                    if (row != null)
                    {
                        Score score = new Score
                        {
                            Id = row.Id,
                            Scores = row.Score,
                            Date = row.Date,
                            Level = row.Level
                        };
                        return score;
                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Score> LoadByScoreId(Guid id)
        {
            try
            {
                List<Score> rows = new List<Score>();
                using (PacMenEntities dc = new PacMenEntities(options))
                {
                    var results = (from s in dc.tblScores
                                   join u in dc.tblUsers on s.Id equals u.ScoreId
                                   where s.Id == id
                                   select new
                                   {
                                       s.Id,
                                       s.Score,
                                       s.Date,
                                       s.Level,
                                       u.UserName
                                   }).ToList();

                    results.ForEach(r => rows.Add(
                         new Score
                         {
                             Id = r.Id,
                             Scores = r.Score,
                             Date = r.Date,
                             Level = r.Level
                         }
                        ));

                    return rows;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
