using Customers.Api.Mapping;
using Customers.Api.Messaging;
using Customers.Api.Models;
using Customers.Api.Repositories;

namespace Customers.Api.Services;

public class CustomerService(CustomerRepository customerRepository, SqsMessenger sqsMessenger)
{
    public async Task AddAsync(Customer customer)
    {
        await customerRepository.AddAsync(customer);
        await sqsMessenger.SendMessageAsync(customer.ToCustomerCreated());
    }

    public async Task UpdateAsync(Customer customer)
    {
        await customerRepository.UpdateAsync(customer);
        await sqsMessenger.SendMessageAsync(customer.ToCustomerUpdated());
    }

    public async Task DeleteAsync(Guid id)
    {
        await customerRepository.DeleteAsync(id);
        await sqsMessenger.SendMessageAsync(id.ToCustomerDeleted());
    }

    public Task<IEnumerable<Customer>> GetAsync()
    {
        return customerRepository.GetAsync();
    }
}