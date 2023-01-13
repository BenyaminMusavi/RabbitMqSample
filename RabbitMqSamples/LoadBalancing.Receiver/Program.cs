using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost",
};

var connection = connectionFactory.CreateConnection();

using var channel = connection.CreateModel();

// تعریف صف
string queueName = "LoadBalancing";
channel.QueueDeclare(queueName, true, false, false, null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += Consumer_Received;
channel.BasicConsume(queueName, false, consumer);
Console.WriteLine("Please [Enter] to exist.");
Console.ReadLine();
void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    var body = e.Body.ToArray();
    var stringMessagee = System.Text.Encoding.UTF8.GetString(body);
    Console.WriteLine($"[-] Processing messager: {stringMessagee}");
    Thread.Sleep(2000);
    Console.WriteLine($"[-] Finish messager: {stringMessagee}");

    channel.BasicAck(e.DeliveryTag, false);
}