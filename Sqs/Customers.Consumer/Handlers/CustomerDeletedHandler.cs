using Customers.Consumer.Messaging;
using MediatR;

namespace Customers.Consumer.Handlers;

public class CustomerDeletedHandler(ILogger<CustomerDeletedHandler> logger) : IRequestHandler<CustomerDeleted>
{
    public Task Handle(CustomerDeleted request, CancellationToken cancellationToken)
    {
        logger.LogInformation(request.Id.ToString());
        return Task.CompletedTask;
    }
}