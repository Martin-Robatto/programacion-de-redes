using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace LogsServer
{
    public class LogReceiver
    {
        public LogReceiver() {}
        
        public async Task Receive()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var channel = factory.CreateConnection().CreateModel();
            channel.QueueDeclare(queue: "log_queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var content  = Encoding.UTF8.GetString(body);
                var log = JsonSerializer.Deserialize<Log>(content);
                LogRepository.Add(log);
            };
            
            channel.BasicConsume(queue: "log_queue",
                autoAck: true,
                consumer: consumer);
        }
    }
}