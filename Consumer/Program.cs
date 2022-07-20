using MassTransit;
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

                sqs.AutoDelete = true;

                sqs.Message<Beacon>(beacon => { beacon.SetEntityName("test-beacon"); });

                sqs.ReceiveEndpoint("test-queue", endpoint =>
                {
                    endpoint.Consumer<Receiver>();
                });

                sqs.ConfigureEndpoints(context, KebabCaseEndpointNameFormatter.Instance);

            });
        });
    })
    .Build().Run();
}
catch (Exception error)
{
    Console.WriteLine(error.ToString());
}