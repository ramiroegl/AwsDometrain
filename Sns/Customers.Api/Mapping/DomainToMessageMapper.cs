using Customers.Api.Messaging;
using Customers.Api.Models;

namespace Customers.Api.Mapping;

public static class DomainToMessageMapper
{
    public static CustomerCreated ToCustomerCreated(this Customer customer)
    {
        return new CustomerCreated
        {
            Id = customer.Id,
            Email = customer.Email,
            Username = customer.Username,
            FullName = customer.FullName,
            DateOfBirth = customer.DateOfBirth
        };
    }
    
    public static CustomerUpdated ToCustomerUpdated(this Customer customer)
    {
        return new CustomerUpdated
        {
            Id = customer.Id,
            Email = customer.Email,
            Username = customer.Username,
            FullName = customer.FullName,
            DateOfBirth = customer.DateOfBirth
        };
    }
    
    public static CustomerDeleted ToCustomerDeleted(this Guid id)
    {
        return new CustomerDeleted
        {
            Id = id
        };
    }
}