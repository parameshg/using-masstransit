using MassTransit;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Diagnostics;

public class Worker : BackgroundService
{
    private IBus Bus { get; }

    public Worker(IBus bus)
    {
        Bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        long counter = 0;

        int pid = Process.GetCurrentProcess().Id;

        while (!token.IsCancellationRequested)
        {
            var beacon = new Beacon { Timestamp = DateTime.Now, Counter = counter, Process = pid };

            await Bus.Publish(beacon, token);

            Console.WriteLine(JsonConvert.SerializeObject(beacon));

            counter++;

            await Task.Delay(1000, token);
        }
    }
}