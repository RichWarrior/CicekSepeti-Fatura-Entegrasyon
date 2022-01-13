using System;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IFileRepository File { get; }
        IInvoiceRepository Invoice { get; }
        IInvoiceStatusRepository InvoiceStatus { get; }
        IParameterRepository Parameter { get; }
        IUserRepository User { get; }

        IRabbitMQService RabbitMQ { get; }

        bool Commit();
        bool SaveChanges();
    }
}
