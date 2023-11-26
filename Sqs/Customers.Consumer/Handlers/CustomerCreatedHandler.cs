using Customers.Consumer.Messaging;
using MediatR;

namespace Customers.Consumer.Handlers;

public class CustomerCreatedHandler(ILogger<CustomerCreatedHandler> logger) : IRequestHandler<CustomerCreated>
{
    public Task Handle(CustomerCreated request, CancellationToken cancellationToken)
    {
        logger.LogInformation(request.FullName);
        return Task.CompletedTask;
    }
}