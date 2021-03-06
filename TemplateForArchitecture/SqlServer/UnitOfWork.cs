﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Exceptions;

namespace SqlServer
{
    public class UnitOfWork : IUnitOfWork, IRepositoryFactory
    {
        #region [Private members]

        private bool _disposed;
        private bool _isTransactionActive;
        private readonly DbContext _context;
        private readonly DbContextTransaction _transaction;

        #endregion


        #region [Ctor's]

        public UnitOfWork(DbContext context)
        {
            _context = context;
            _transaction = _context.Database.BeginTransaction();
            _isTransactionActive = true;
        }

        #endregion


        #region Implementation of IDisposable

        public void Dispose()
        {
            if (_isTransactionActive)
            {
                try
                {
                    _context.SaveChanges();
                    _transaction.Commit();
                    _isTransactionActive = false;
                }
                catch (Exception e)
                {
                    _transaction.Rollback();
                    _context.Dispose();
                    _disposed = true;
                    _isTransactionActive = false;
                    throw new RepositoryException(e);
                }
            }
            if (!_disposed)
            {
                _context.Dispose();
                _disposed = true;
            }
        }

        #endregion


        #region Implementation of IUnitOfWork

        public void Commit()
        {
            try
            {
                if (_isTransactionActive && !_disposed)
                {
                    _context.SaveChanges();
                    _transaction.Commit();
                    _isTransactionActive = false;
                }
            }
            catch (Exception e)
            {
                _transaction.Rollback();
                _isTransactionActive = false;
                throw new RepositoryException(e.Message);
            }
        }

        public void Rollback()
        {
            if (_isTransactionActive)
            {
                try
                {
                    _context.SaveChanges();
                    _transaction.Commit();
                    _isTransactionActive = false;
                }
                catch (Exception e)
                {
                    _transaction.Rollback();
                    _isTransactionActive = false;

                    _context.Dispose();
                    _disposed = true;

                    throw new RepositoryException(e);
                }
            }
            if (!_disposed)
            {
                _context.Dispose();
                _disposed = true;
            }
        }

        public void PreSave()
        {
            _context.SaveChanges();
        }

        #endregion
    }
}
