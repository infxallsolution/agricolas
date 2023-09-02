using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using Nomina.WebForms.Repository.Repository.Interface;

namespace Nomina.WebForms.Repository.Repository.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Private Fields

        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IUnitOfWork _unitOfWork;
        #endregion Private Fields

        public Repository(DbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            if (_context != null)
            {
                _dbSet = _context.Set<TEntity>();
            }
        }

        public virtual TEntity Find(params object[] keyValues) => _dbSet.Find(keyValues);

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            var query = ApplyDefaultFilters(_dbSet);
            return query.FirstOrDefault(predicate);
        }

        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = ApplyDefaultFilters(_dbSet);
            return query.FirstOrDefaultAsync(predicate);
        }

        public TEntity Find<TKey>(Expression<Func<TEntity, TKey>> sortExpression, bool isDesc, Expression<Func<TEntity, bool>> predicate) => isDesc ? _dbSet.OrderBy(sortExpression).FirstOrDefault(predicate) : _dbSet.OrderByDescending(sortExpression).FirstOrDefault(predicate);



        public TEntity InsertAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            var entityDb = _dbSet.Add(entity);
            _unitOfWork.SyncObjectState(entity);
            return entityDb;
        }

        public virtual void Insert(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _dbSet.Add(entity);
            _unitOfWork.SyncObjectState(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _unitOfWork.SyncObjectState(entity);
            return Task.FromResult(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _unitOfWork.SyncObjectState(entity);
        }


        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _unitOfWork.SyncObjectState(entity);
        }

        public virtual IQueryable<TEntity> GetAll(bool withoutDefaultFilters = false)
        {
            IQueryable<TEntity> query = _dbSet;

            var query2 = ApplyDefaultFilters(_dbSet);
            return query2;
        }

        public virtual DbSet<TEntity> Get() => _dbSet;

        public IQueryable<TEntity> Queryable() => _dbSet;

        public IRepository<T> GetRepository<T>() where T : class => _unitOfWork.Repository<T>();

        public virtual async Task<TEntity> FindAsync(params object[] keyValues) => await _dbSet.FindAsync(keyValues);

        public virtual async Task<bool> DeleteAsync(params object[] keyValues) => await DeleteAsync(CancellationToken.None, keyValues);

        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await FindAsync(cancellationToken, keyValues);

            if (entity == null)
            {
                return false;
            }

            _context.Entry(entity).State = EntityState.Deleted;
            //_context.Set<TEntity>().Attach(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }


        public int Commit()
        {
            return _unitOfWork.SaveChanges();
        }

        public IQueryable<TEntity> ApplyDefaultFilters(IQueryable<TEntity> query)
        {
            return query;
        }

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includes = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        public int RunProcedure(string name, object parameters = null)
        {
            throw new NotImplementedException();
        }
    }
}