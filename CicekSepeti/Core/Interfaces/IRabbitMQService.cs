using RabbitMQ.Client;

namespace Core.Interfaces
{
    public interface IRabbitMQService
    {
        IConnection GetRabbitMQConnection();
    }
}
