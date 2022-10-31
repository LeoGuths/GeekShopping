using GeekShopping.MessageBus;

namespace GeekShopping.OrderAPI.RabbitMqSender; 

public interface IRabbitMqMessageSender {
    void SendMessage(BaseMessage baseMessage, string queueName);
}