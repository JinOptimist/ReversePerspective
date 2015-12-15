using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Xml.Linq;
using DAO.Model;

namespace DAO.Repository
{
    public class OpusRepository<T> where T : BaseModel
    {
        internal readonly ReversePerspectiveContext _db = new ReversePerspectiveContext();

        public T Get(long id)
        {
            IQueryable<T> query = _db.Set<T>();
            return query.SingleOrDefault(x => x.Id == id);
        }

        public List<T> GetAll()
        {
            IQueryable<T> query = _db.Set<T>();
            return query.ToList();
        }

        public void Save(T entity)
        {
            if (entity.Id > 0)
            {
                _db.Set<T>().Attach(entity);
                _db.Entry(entity).State = EntityState.Modified;
                _db.SaveChanges();
                return;
            }

            _db.Set<T>().Add(entity);
            _db.SaveChanges();
        }

        public void Remove(T entity)
        {
            _db.Set<T>().Remove(entity);
            _db.SaveChanges();
        }

        public void Remove(long id)
        {
            _db.Set<T>().Remove(Get(id));
            _db.SaveChanges();
        }
    }
}