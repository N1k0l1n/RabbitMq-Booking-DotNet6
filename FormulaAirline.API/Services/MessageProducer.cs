using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FormulaAirline.API.Services
{
    public class MessageProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            //Starting Communication with RabbitMq (configuration)
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "mypass",
                VirtualHost = "/"
            };

            //Initiate Connection
            var connection = factory.CreateConnection();

            //Specify a Channel (it creates a new Channel)
            using var channel = connection.CreateModel();

            //Create a Queue
            channel.QueueDeclare("bookings", durable: true, exclusive: false);

            //Initiate Serialization
            var jsonSting = JsonSerializer.Serialize(message);
            //Encoding
            var body = Encoding.UTF8.GetBytes(jsonSting);


            //Publish Message
            channel.BasicPublish("", "bookings", body : body);
        }
    }
}
