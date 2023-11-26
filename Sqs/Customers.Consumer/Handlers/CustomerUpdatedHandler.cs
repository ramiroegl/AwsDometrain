using Customers.Consumer.Messaging;
using MediatR;

namespace Customers.Consumer.Handlers;

public class CustomerUpdatedHandler(ILogger<CustomerUpdatedHandler> logger) : IRequestHandler<CustomerUpdated>
{
    public Task Handle(CustomerUpdated request, CancellationToken cancellationToken)
    {
        logger.LogInformation(request.FullName);
        return Task.CompletedTask;
    }
}