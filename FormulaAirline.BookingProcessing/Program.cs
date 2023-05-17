// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Welcome to the Booking Service");

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


var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) =>
{
    //getting my byte[]
    var body = eventArgs.Body.ToArray();

    //convert byte[] to text
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"New Booking Processing is initiated for - {message}");
};

channel.BasicConsume("bookings", true, consumer);

Console.ReadKey();