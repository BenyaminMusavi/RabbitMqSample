using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost",
};

var connection = connectionFactory.CreateConnection();
using var channel = connection.CreateModel();
string exchengeName = "MyMessageExchenge";
channel.ExchangeDeclare(exchengeName, ExchangeType.Fanout, false, false, null);

var  queueName=channel.QueueDeclare().QueueName;
channel.QueueBind(queueName, exchengeName, "", null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += Consumer_Received;
channel.BasicConsume(queueName, true, consumer);
Console.WriteLine("Please [Enter] to exist.");
Console.ReadLine();

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    var body = e.Body.ToArray();
    var stringMessagee = System.Text.Encoding.UTF8.GetString(body);
    Console.WriteLine($"[-] start Processing using receiver: {stringMessagee}");

}