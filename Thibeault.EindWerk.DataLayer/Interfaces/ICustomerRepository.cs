using Thibeault.EindWerk.Objects;

namespace Thibeault.EindWerk.DataLayer
{
    public interface ICustomerRepository
    {
        Task<Customer> AddCustomer();
        Task<bool> DeleteCustomer(string trackingNumber);
        Task DisposeAsync();
        Task<Customer> GetCustomerByTrackingNumber(string trackingNumber);
        Task<List<Customer>> GetCustomers();
        Task SaveAsync();
        Task<Customer> UpdateCustomer(Customer customerToUpdate);
    }
}