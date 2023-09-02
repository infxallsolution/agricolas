using Nomina.WebForms.Repository.Repository.Implementation.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Nomina.WebForms.Repository.Repository.Interface;
using System.Data.Entity;

namespace Nomina.WebForms.Repository.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private DbContext _context;
        private DbContextTransaction _transaction;
        private Dictionary<string, dynamic> _repositories;


        public UnitOfWork(DbContext context)
        {
            _context = context;
            _repositories = new Dictionary<string, dynamic>();
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            var connection = _context.Database.Connection;
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            _transaction = _context.Database.BeginTransaction(isolationLevel);
        }

        public bool Commit()
        {
            _transaction.Commit();
            return true;
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                try
                {
                    if (_context != null)
                    {
                        _context.Dispose();
                        _context = null;
                    }
                }
                catch (ObjectDisposedException)
                {
                    // 
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            SyncObjectsStatePostCommit();
        }

        public int SaveChanges()
        {
            SyncObjectsStatePreCommit();
            var changes = _context.SaveChanges();
            SyncObjectsStatePostCommit();
            return changes;
        }

        public void SyncObjectsStatePostCommit()
        {
            foreach (var dbEntityEntry in _context.ChangeTracker.Entries())
            {
                dbEntityEntry.State = StateHelper.ConvertState(dbEntityEntry.State);
            }
        }

        public void SyncObjectsStatePreCommit()
        {
            throw new NotImplementedException();
        }

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }
    }
}