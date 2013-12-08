using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Infrastructure.Guard;
using Core.Entities;
using Core.Exceptions;
using Core.InterfaceRepository;

namespace SqlServer.Repositories
{
    public class Repository<TEntity, TKey> : Repository, IRepository<TEntity, TKey> where TEntity : Entity
    {
        private readonly DbSet<TEntity> _entities; 

        public Repository(DbContext context) : base(context)
        {
            _entities = Context.Set<TEntity>();
        }

        #region Implementation of IRepository<TEntity,TKey>

        public void Create(TEntity value)
        {
            Guard.AgainstNullReference(value, "value");

            _entities.Add(value);
        }

        public void Update(TEntity value)
        {
            Guard.AgainstNullReference(value, "value");

            _entities.Attach(value);
            Context.Entry(value).State = EntityState.Modified;
        }

        public void Remove(TEntity value)
        {
            Guard.AgainstNullReference(value, "value");

            _entities.Remove(value);
        }

        public TEntity GetEntityById(TKey id)
        {
            Guard.AgainstNullReference(id, "id");

            try
            {
                return _entities.Find(id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            Guard.AgainstNullReference(predicate, "predicate");

            try
            {
                return _entities.Where(predicate).SingleOrDefault();
            }
            catch (Exception e)
            {
                throw new RepositoryException(e.Message);
            }
        }

        public IQueryable<TEntity> All()
        {
            return _entities;
        }

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            Guard.AgainstNullReference(predicate, "predicate");

            return _entities.Where(predicate);
        }

        #endregion
    }
}
