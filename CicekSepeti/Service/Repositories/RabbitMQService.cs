using Core.Interfaces;
using Core.Utilities;
using RabbitMQ.Client;

namespace Service.Repositories
{
    public class RabbitMQService : IRabbitMQService
    {
        public IConnection GetRabbitMQConnection()
        {
            ConnectionInfo connectionInfo = ConnectionInfo.Instance;

            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = connectionInfo.RabbitMQHost,
                UserName = connectionInfo.RabbitMQUser,
                Password = connectionInfo.RabbitMQPassword,
                Port = 5672
            };

            return connectionFactory.CreateConnection();
        }
    }
}
