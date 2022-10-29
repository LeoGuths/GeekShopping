using System.Text;
using System.Text.Json;
using GeekShopping.OrderAPI.Messages;
using GeekShopping.OrderAPI.Model;
using GeekShopping.OrderAPI.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GeekShopping.OrderAPI.MessageConsumer; 

public class RabbitMqCheckoutConsumer : BackgroundService {
    private readonly OrderRepository _repository;
    private IConnection _connection;
    private IModel _channel;

    public RabbitMqCheckoutConsumer(OrderRepository repository) {
        _repository = repository;
        var factory = new ConnectionFactory {
            HostName = "127.0.0.1",
            UserName = "guest",
            Password = "guest"
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "checkoutQueue", false, false, false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken) {
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (channel, evt) => {
            var content = Encoding.UTF8.GetString(evt.Body.ToArray());
            CheckoutHeaderVO vo = JsonSerializer.Deserialize<CheckoutHeaderVO>(content);
            ProcessOrder(vo).GetAwaiter().GetResult();
            _channel.BasicAck(evt.DeliveryTag, false);
        };
        _channel.BasicConsume("checkoutQueue", false, consumer);
        return Task.CompletedTask;
    }

    private async Task ProcessOrder(CheckoutHeaderVO vo) {
        OrderHeader order = new () {
            UserId = vo.UserId,
            CouponCode =  vo.CouponCode,
            PurchaseAmount = vo.PurchaseAmout,
            DiscountAmount = vo.DiscountAmount,
            FirstName = vo.FirstName,
            LastName = vo.LastName,
            OrderTime = DateTime.UtcNow,
            Phone = vo.Phone,
            Email = vo.Email,
            CardNumber = vo.CardNumber,
            CardCvv = vo.CardCvv,
            ExpiryMonthYear = vo.CardExpiryMonthYear, 
            CartTotalItems = vo.CartTotalItems,
            OrderDetails = new List<OrderDetail>()
        };
        
        vo.CartDetails?.ForEach(details => {
            OrderDetail detail = new() {
                ProductId = details.ProductId,
                ProductName = details.Product.Name,
                Price = details.Product.Price,
                Count = details.Count
            };
            order.CartTotalItems += details.Count;
            order.OrderDetails.Add(detail);
        });

        await _repository.AddOrder(order);
    }
}