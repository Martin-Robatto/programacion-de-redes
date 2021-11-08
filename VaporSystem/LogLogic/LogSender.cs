using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain;
using RabbitMQ.Client;

namespace ConsoleServer
{
    public class LogSender
    {
        private IModel channel;
        private static LogSender _instance;
        public static LogSender Instance
        {
            get { return GetInstance(); }
        }
        
        private LogSender(){}
        
        private static LogSender GetInstance()
        {
            if (_instance is null)
            {
                _instance = new LogSender();
            }
            return _instance;
        }

        public async Task StartLogsRecord()
        {
            var local_channel = new ConnectionFactory() {HostName = "localhost"}.CreateConnection().CreateModel();
            local_channel.QueueDeclare(queue: "log_queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            this.channel = local_channel;
        }
        
        public void SendLog(Log logToSave)
        {
            var stringLog = JsonSerializer.Serialize(logToSave);
            var body = Encoding.UTF8.GetBytes(stringLog);
            channel.BasicPublish(exchange: "",
                routingKey: "log_queue",
                basicProperties: null,
                body: body);
        }
    }
}