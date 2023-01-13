using RabbitMQ.Client;

var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost",
};

var connection = connectionFactory.CreateConnection();

using var channel = connection.CreateModel();

// sender
string queueName = "HelloWorld";
channel.QueueDeclare(queueName, false, false, false, null);

Console.Write("Type ypur messgae and press [Enter]:");
string message = Console.ReadLine() ?? "Default message";

var messageBytes = System.Text.Encoding.UTF8.GetBytes(message).ToArray();

//  
channel.BasicPublish("", queueName, null, messageBytes);

Console.WriteLine($"[*] your message sent:{message}");

Console.WriteLine("Please [Enter] to exist.");

Console.ReadLine();