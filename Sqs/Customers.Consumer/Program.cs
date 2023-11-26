using Amazon.SQS;
using Customers.Consumer;
using Customers.Consumer.Messaging;

var builder = Host.CreateApplicationBuilder(args);
builder.Services
    .Configure<QueueSettings>(builder.Configuration.GetRequiredSection(nameof(QueueSettings)))
    .AddSingleton<IAmazonSQS, AmazonSQSClient>()
    .AddHostedService<QueueConsumerWorker>()
    .AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Program>());

var host = builder.Build();
host.Run();