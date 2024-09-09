using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PacMen.BL
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string message) : base(message) { }
        public AlreadyExistsException() : base("Row already exists.") { }
    }

    public abstract class GenericManager<T> where T : class, IEntity
    {
        protected DbContextOptions<PacMenEntities> options;
        protected readonly ILogger logger;

        public GenericManager(DbContextOptions<PacMenEntities> options)
        {
            this.options = options;
        }

        public GenericManager(ILogger logger,
                              DbContextOptions<PacMenEntities> options)
        {
            this.options = options;
            this.logger = logger;
        }

        public GenericManager() { }

        public static string[,] ConvertData<U>(List<U> entities, string[] columns) where U : class
        {
            string[,] data = new string[entities.Count + 1, columns.Length];

            int counter = 0;
            for (int i = 0; i < columns.Length; i++)
            {
                data[counter, i] = columns[i];
            }
            counter++;


            foreach (var entity in entities)
            {
                for (int i = 0; i < columns.Length; i++)
                {
                    data[counter, i] = entity.GetType().GetProperty(columns[i]).GetValue(entity, null).ToString();
                }
                counter++;
            }
            return data;
        }

        public async Task<List<T>> LoadAsync()
        {
            try
            {
                if (logger != null) logger.LogWarning($"Get {typeof(T).Name}s");
                var rows = new PacMenEntities(options)
                    .Set<T>()
                    .ToListAsync<T>()
                    .ConfigureAwait(false);

                return await rows;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<T> Load()
        {
            try
            {
                if (logger != null) logger.LogWarning($"Get {typeof(T).Name}s");
                return new PacMenEntities(options)
                    .Set<T>()
                    .ToList<T>()
                    .ToList<T>();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<T> Load(string storedproc)
        {
            try
            {
                return new PacMenEntities(options)
                    .Set<T>()
                    .FromSqlRaw($"exec {storedproc}")
                    .ToList<T>()
                    .ToList<T>();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<T> Load(string storedproc, string value)
        {
            try
            {
                return new PacMenEntities(options)
                    .Set<T>()
                    .FromSqlRaw($"exec {storedproc} {value}")
                    .ToList<T>()
                    .ToList<T>();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public T LoadById(Guid id)
        {
            try
            {
                var row = new PacMenEntities(options)
                    .Set<T>()
                    .Where(t => t.Id == id)
                    .FirstOrDefault();
                return row;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<T> LoadByIdAsync(Guid id)
        {
            try
            {
                if (logger != null) logger.LogWarning($"GetById {typeof(T).Name}s");

                var rows = await new PacMenEntities(options)
                    .Set<T>()
                    .Where(t => t.Id == id)
                    .ToListAsync<T>()
                    .ConfigureAwait(false);

                return rows[0];
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int Insert(T entity,
                          Expression<Func<T, bool>> predicate = null,
                          bool rollback = false)
        {
            try
            {
                int results = 0;
                using (PacMenEntities dc = new PacMenEntities(options))
                {
                    if ((predicate == null) || ((predicate != null) && (!dc.Set<T>().Any(predicate))))
                    {
                        IDbContextTransaction dbTransaction = null;
                        if (rollback) dbTransaction = dc.Database.BeginTransaction();

                        entity.Id = Guid.NewGuid();

                        dc.Set<T>().Add(entity);
                        results = dc.SaveChanges();

                        if (rollback) dbTransaction.Rollback();
                    }
                    else
                    {
                        if (logger != null) logger.LogWarning("Row already exists. {UserId}", "bfoote");
                        throw new Exception("Row already exists.");
                    }

                }

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Guid> InsertAsync(T entity,
                                            Expression<Func<T, bool>> predicate = null,
                                            bool rollback = false)
        {
            try
            {
                Guid results = Guid.Empty;
                using (PacMenEntities dc = new PacMenEntities(options))
                {
                    if ((predicate == null) || ((predicate != null) && (!dc.Set<T>().Any(predicate))))
                    {
                        IDbContextTransaction dbTransaction = null;
                        if (rollback) dbTransaction = dc.Database.BeginTransaction();

                        entity.Id = Guid.NewGuid();

                        dc.Set<T>().Add(entity);
                        int rowsAffected = dc.SaveChanges();

                        if (rollback) dbTransaction.Rollback();
                    }
                    else
                    {
                        if (logger != null) logger.LogWarning("Row already exists. {UserId}", "araosas");
                        throw new AlreadyExistsException("That row already exists.");
                    }

                }

                return entity.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Insert(T entity,
                         bool rollback = false)
        {
            try
            {
                int results = 0;
                using (PacMenEntities dc = new PacMenEntities(options))
                {

                    IDbContextTransaction dbTransaction = null;
                    if (rollback) dbTransaction = dc.Database.BeginTransaction();

                    entity.Id = Guid.NewGuid();

                    dc.Set<T>().Add(entity);
                    results = dc.SaveChanges();

                    if (rollback) dbTransaction.Rollback();


                }

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int Update(T entity, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (PacMenEntities dc = new PacMenEntities(options))
                {
                    IDbContextTransaction dbTransaction = null;
                    if (rollback) dbTransaction = dc.Database.BeginTransaction();

                    dc.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    results = dc.SaveChanges();

                    if (rollback) dbTransaction.Rollback();

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
                using (PacMenEntities dc = new PacMenEntities(options))
                {
                    IDbContextTransaction dbTransaction = null;
                    if (rollback) dbTransaction = dc.Database.BeginTransaction();

                    T row = dc.Set<T>().FirstOrDefault(t => t.Id == id);

                    if (row != null)
                    {
                        dc.Set<T>().Remove(row);
                        results = dc.SaveChanges();
                        if (rollback) dbTransaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row does not exist.");
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
