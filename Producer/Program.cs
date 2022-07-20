using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

try
{
    Console.WriteLine($"Hello, World! [PID: {Process.GetCurrentProcess().Id}]");

    Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
    {
        services.AddMassTransit(cfg =>
        {
            cfg.UsingAmazonSqs((context, sqs) =>
            {
                sqs.Host("us-west-2", host =>
                {
                    host.AccessKey(Constants.AWS_ACCESS_KEY_ID);
                    host.SecretKey(Constants.AWS_SECRET_ACCESS_KEY);
                });

                sqs.Message<Beacon>(beacon => { beacon.SetEntityName("test-beacon"); });

                sqs.ConfigureEndpoints(context, KebabCaseEndpointNameFormatter.Instance);
            });
        });

        services.AddHostedService<Worker>();
    })
    .Build().Run();
}
catch (Exception error)
{
    Console.WriteLine(error.ToString());
}