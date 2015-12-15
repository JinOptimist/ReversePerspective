using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAO.Model;

namespace DAO.Repository
{
    public class BaseRepository<T> where T : BaseModel
    {
        internal readonly ReversePerspectiveContext Db;
        public BaseRepository(ReversePerspectiveContext context)
        {
            Db = context;
        }

        public T Get(long id)
        {
            IQueryable<T> query = Db.Set<T>();
            return query.SingleOrDefault(x => x.Id == id);
        }

        public List<T> GetAll()
        {
            IQueryable<T> query = Db.Set<T>();
            return query.ToList();
        }

        public void Save(T entity)
        {
            if (entity.Id > 0)
            {
                Db.Set<T>().Attach(entity);
                Db.Entry(entity).State = EntityState.Modified;
                Db.SaveChanges();
                return;
            }

            Db.Set<T>().Add(entity);
            Db.SaveChanges();
        }

        public void Remove(T entity)
        {
            Db.Set<T>().Remove(entity);
            Db.SaveChanges();
        }

        public void Remove(long id)
        {
            var entity = Db.Set<T>().SingleOrDefault(x => x.Id == id);
            Db.Set<T>().Remove(entity);
            Db.SaveChanges();
        }
    }
}