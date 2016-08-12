using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.GenericRepos
{
    public abstract class GenericEFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext context;
        public GenericEFRepository(DbContext context)
        {
            this.context = context;
        }

        public void Delete(TEntity entityToDelete)
        {
            context.Set<TEntity>().Remove(entityToDelete);
            context.Entry<TEntity>(entityToDelete).State = EntityState.Deleted;
            //az állapot amúgy automatikusan állítódik át addnál és removenál is
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            TEntity entityToDelete = GetById(id);
            if (entityToDelete == null)
            {
                throw new ArgumentException("No Data");
            }
            Delete(entityToDelete);
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return GetAll().Where(filter);
        }

        public IQueryable<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }

        abstract public TEntity GetById(int id);

        public virtual void Insert(TEntity newEntity)
        {
            //lazy loading
            context.Set<TEntity>().Add(newEntity);
            context.Entry<TEntity>(newEntity).State = EntityState.Added;
            context.SaveChanges();
        }
    }
}
