using ConsumerAPI.Services;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SharedLibrary.Models;
using System.Text;

namespace ConsumerAPI.BackgroundServices
{
    public class MessageListenerService : BackgroundService
    {
        private readonly ILogger<MessageListenerService> _logger;
        private IConnection _connection;
        private RabbitMQ.Client.IModel _channel;
        private readonly DatabaseService _databaseService;

        public MessageListenerService(ILogger<MessageListenerService> logger, DatabaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
            InitializeRabbitMQListener();
        }

        private void InitializeRabbitMQListener()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" }; // Replace with actual host if necessary
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "repairRequestsQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _channel.BasicQos(0, 1, false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var repairRequest = JsonConvert.DeserializeObject<RepairRequest>(content);

                HandleMessage(repairRequest);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume("repairRequestsQueue", false, consumer);

            return Task.CompletedTask;
        }

        private void HandleMessage(RepairRequest request)
        {
            try
            {
                // Process the message
                _logger.LogInformation($"Received repair request: {request.Id}");
                _databaseService.SaveRepairRequest(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message.");
            }
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
