using Customers.Api.Models;

namespace Customers.Api.Repositories;

public class CustomerRepository
{
    private readonly List<Customer> _customers = [];

    public Task AddAsync(Customer customer)
    {
        if (_customers.Any(x => x.Id == customer.Id))
        {
            throw new Exception("Customer already exists");
        }
        _customers.Add(customer);
        return Task.CompletedTask;
    }

    public async Task UpdateAsync(Customer customer)
    {
        var oldCustomer = _customers.Single(x => x.Id == customer.Id);
        await DeleteAsync(customer.Id);
        await AddAsync(customer);
    }

    public Task DeleteAsync(Guid id)
    {
        var oldCustomer = _customers.Single(x => x.Id == id);
        _customers.Remove(oldCustomer);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Customer>> GetAsync()
    {
        return Task.FromResult(_customers.Select(customer => customer));
    }
}