using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain;
using RabbitMQ.Client;
using SettingsLogic;
using SettingsLogic.Interface;

namespace ConsoleServer.LogsLogic
{
    public class LogSender
    {
        private readonly ISettingsManager _settingsManager = new SettingsManager();
        private IModel _channel;
        
        private static LogSender _instance;
        public static LogSender Instance
        {
            get { return GetInstance(); }
        }

        private LogSender() { }
        
        private static LogSender GetInstance()
        {
            if (_instance is null)
            {
                _instance = new LogSender();
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
        
        public void SendLog(Log logToSave)
        {
            var queue = _settingsManager.ReadSetting(ServerConfig.LogQueueConfigKey);
            var stringLog = JsonSerializer.Serialize(logToSave);
            var body = Encoding.UTF8.GetBytes(stringLog);
            _channel.BasicPublish(exchange: string.Empty,
                routingKey: queue,
                basicProperties: null,
                body: body);
        }
    }
}