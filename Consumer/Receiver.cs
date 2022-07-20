using MassTransit;
using Newtonsoft.Json;

public class Receiver : IConsumer<Beacon>
{
    public Task Consume(ConsumeContext<Beacon> context)
    {
        Console.WriteLine(JsonConvert.SerializeObject(context.Message));

        return Task.CompletedTask;
    }
}