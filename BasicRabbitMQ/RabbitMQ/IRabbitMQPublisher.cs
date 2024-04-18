namespace BasicRabbitMQ.RabbitMQ;

public interface IRabbitMQPublisher
{
    public void SendMessage<T>(T message);
}