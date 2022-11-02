﻿using GeekShopping.MessageBus;

namespace GeekShopping.PaymentAPI.RabbitMqSender; 

public interface IRabbitMqMessageSender {
    void SendMessage(BaseMessage baseMessage);
}