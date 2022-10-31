using System.Text;
using System.Text.Json;
using GeekShopping.MessageBus;
using GeekShopping.PaymentAPI.Messages;
using RabbitMQ.Client;

namespace GeekShopping.PaymentAPI.RabbitMqSender; 

public class RabbitMqMessageSender : IRabbitMqMessageSender {
    private readonly string _hostName;
    private readonly string _password;
    private readonly string _userName;
    private IConnection _connection;

    public RabbitMqMessageSender() {
        _hostName = "127.0.0.1";
        _password = "guest";
        _userName = "guest";
    }

    public void SendMessage(BaseMessage message, string queueName) {
        if (ConnectionExists()) {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
            var body = GetMessageAsByteArray(message);
            channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: null, body: body);   
        }
    }

    private byte[] GetMessageAsByteArray(BaseMessage message) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize((UpdatePaymentResultMessage)message, options);
        return Encoding.UTF8.GetBytes(json);
    }

    private bool ConnectionExists() {
        if (_connection != null) return true;
        CreateConnection();
        return _connection != null;
    }

    private void CreateConnection() {
        try {
            var factory = new ConnectionFactory {
                HostName = _hostName,
                UserName = _userName,
                Password = _password
            };
            _connection = factory.CreateConnection();
        }
        catch (Exception e) {
            Console.WriteLine(e); // TODO: Logger
            throw;
        }
    }
}