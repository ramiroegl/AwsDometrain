using Customers.Api.Mapping;
using Customers.Api.Messaging;
using Customers.Api.Models;
using Customers.Api.Repositories;

namespace Customers.Api.Services;

public class CustomerService(CustomerRepository customerRepository, SnsMessenger snsMessenger)
{
    public async Task AddAsync(Customer customer)
    {
        await customerRepository.AddAsync(customer);
        await snsMessenger.PublishMessageAsync(customer.ToCustomerCreated());
    }

    public async Task UpdateAsync(Customer customer)
    {
        await customerRepository.UpdateAsync(customer);
        await snsMessenger.PublishMessageAsync(customer.ToCustomerUpdated());
    }

    public async Task DeleteAsync(Guid id)
    {
        await customerRepository.DeleteAsync(id);
        await snsMessenger.PublishMessageAsync(id.ToCustomerDeleted());
    }

    public Task<IEnumerable<Customer>> GetAsync()
    {
        return customerRepository.GetAsync();
    }
}