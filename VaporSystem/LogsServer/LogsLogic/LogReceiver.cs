using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SettingsLogic;
using SettingsLogic.Interface;

namespace LogsServer.LogsLogic
{
    public class LogReceiver
    {
        private readonly ISettingsManager _settingsManager = new SettingsManager();
        private IModel _channel;
        
        private static LogReceiver _instance;
        public static LogReceiver Instance
        {
            get { return GetInstance(); }
        }

        private LogReceiver() { }
        
        private static LogReceiver GetInstance()
        {
            if (_instance is null)
            {
                _instance = new LogReceiver();
            }
            return _instance;
        }

        public void Connect()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var channel = factory.CreateConnection().CreateModel();
            var queue = _settingsManager.ReadSetting(ServerConfig.LogQueueConfigKey);
            channel.QueueDeclare(queue: queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            this._channel = channel;
        }
        
        public void ReceiveLogs()
        {
            var consumer = new EventingBasicConsumer(_channel);
            
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var content  = Encoding.UTF8.GetString(body);
                var log = JsonSerializer.Deserialize<Log>(content);
                LogRepository.Instance.Add(log);
            };
            
            var queue = _settingsManager.ReadSetting(ServerConfig.LogQueueConfigKey);
            _channel.BasicConsume(queue: queue,
                autoAck: true,
                consumer: consumer);
        }
    }
}