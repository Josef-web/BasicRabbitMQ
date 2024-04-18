using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace BasicRabbitMQ.RabbitMQ;

public class RabbitMQPublisher:IRabbitMQPublisher
{
    private readonly IConfiguration _configuration;

    public RabbitMQPublisher(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendMessage<T>(T message)
    {
        var connectionHost = _configuration.GetSection("RabbitMQConfiguration:Connection").Value;
        var factory = new ConnectionFactory
        {
            HostName = connectionHost
        };

        var connection = factory.CreateConnection();

        using var channel = connection.CreateModel();
        channel.QueueDeclare("student",exclusive:false,autoDelete:false);
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(exchange:"",routingKey:"student",body:body);
    }
}