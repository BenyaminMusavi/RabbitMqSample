using RabbitMQ.Client;

var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost",
};

var connection = connectionFactory.CreateConnection();

using var channel = connection.CreateModel();

// sender
string queueName = "LoadBalancing";
channel.QueueDeclare(queueName, true, false, false, null);

Console.Write("Type ypur messgae and press [Enter]:");
string message = Console.ReadLine() ?? "Default message";

// ham saf durable ... va ham payam ro set miknm k Persistent bashe
var messageProperty = channel.CreateBasicProperties();
messageProperty.Persistent = true;

for (int i = 0; i < 20; i++)
{
    var messageBytes = System.Text.Encoding.UTF8.GetBytes(message + i).ToArray();
    channel.BasicPublish("", queueName, messageProperty, messageBytes);

}
//

Console.WriteLine($"[*] your message sent:{message}");

Console.WriteLine("Please [Enter] to exist.");

Console.ReadLine();