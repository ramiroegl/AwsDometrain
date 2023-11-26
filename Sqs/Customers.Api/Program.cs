using Amazon.SQS;
using Customers.Api.Messaging;
using Customers.Api.Models;
using Customers.Api.Repositories;
using Customers.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .Configure<QueueSettings>(builder.Configuration.GetRequiredSection(nameof(QueueSettings)))
    .AddSingleton<IAmazonSQS, AmazonSQSClient>()
    .AddSingleton<SqsMessenger>()
    .AddSingleton<CustomerRepository>()
    .AddSingleton<CustomerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/customers", (CustomerService service) => service.GetAsync())
    .WithName("GetCustomers")
    .WithOpenApi();

app.MapPost("/customers", (Customer customer, CustomerService service) => service.AddAsync(customer))
    .WithName("NewConsumer")
    .WithOpenApi();

app.MapPut("/customers", (Customer customer, CustomerService service) => service.UpdateAsync(customer))
    .WithName("UpdateConsumer")
    .WithOpenApi();

app.MapDelete("/customers/{id:guid}", (Guid id, CustomerService service) => service.DeleteAsync(id))
    .WithName("DeleteConsumer")
    .WithOpenApi();

app.Run();