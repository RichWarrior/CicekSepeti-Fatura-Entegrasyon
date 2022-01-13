using Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Service.Repositories;
using System;
using System.Diagnostics;

namespace Service
{
    public class UnitOfWork : IUnitOfWork
    {
        DataContext _context;
        IDbContextTransaction _transaction;

        IFileRepository _fileRepository;
        IInvoiceRepository _invoiceRepository;
        IInvoiceStatusRepository _invoiceStatusRepository;
        IParameterRepository _parameterRepository;
        IUserRepository _userRepository;
        IRabbitMQService _rabbitmqService;

        bool _disposed;

        public UnitOfWork()
        {
            try
            {
                _context = new DataContext();                
                _transaction = _context.Database.BeginTransaction();
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public IFileRepository File => _fileRepository ?? (_fileRepository = new FileRepository(_context));

        public IInvoiceRepository Invoice => _invoiceRepository ?? (_invoiceRepository = new InvoiceRepository(_context));

        public IInvoiceStatusRepository InvoiceStatus => _invoiceStatusRepository ?? (_invoiceStatusRepository = new InvoiceStatusRepository(_context));

        public IParameterRepository Parameter => _parameterRepository ?? (_parameterRepository = new ParameterRepository(_context));

        public IUserRepository User => _userRepository ?? (_userRepository = new UserRepository(_context));

        public IRabbitMQService RabbitMQ => _rabbitmqService ?? (_rabbitmqService = new RabbitMQService());

        public bool Commit()
        {
            bool rtn = false;
            try
            {
                _transaction.Commit();
                rtn = true;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                _transaction.Rollback();
            }
            finally
            {                
                _transaction.Dispose();
                _transaction = _context.Database.BeginTransaction();
                resetRepositories();
            }
            return rtn;
        }

        public bool SaveChanges()
        {
            bool rtn = false;
            try
            {
                _context.SaveChanges();
                rtn = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return rtn;
        }

        private void resetRepositories()
        {
            _fileRepository = null;
            _invoiceRepository = null;
            _invoiceStatusRepository = null;
            _parameterRepository = null;
            _userRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }

                    if (_context != null)
                    {
                        _context.Dispose();
                        _context = null;
                    }

                    resetRepositories();
                }

                _disposed = true;

            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
