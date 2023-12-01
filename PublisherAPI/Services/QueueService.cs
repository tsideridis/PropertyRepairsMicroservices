using Newtonsoft.Json;
using RabbitMQ.Client;
using SharedLibrary.Models;
using System.Text;
using System.Text.Json;

namespace PublisherAPI.Services
{
    public class QueueService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public QueueService()
        {
            var factory = new ConnectionFactory() { HostName = "testlocalhost" }; 
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "repairRequestsQueue",
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void PublishMessage(RepairRequest message)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            _channel.BasicPublish(exchange: "",
                                  routingKey: "repairRequestsQueue",
                                  basicProperties: null,
                                  body: body);
        }

        public void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}
