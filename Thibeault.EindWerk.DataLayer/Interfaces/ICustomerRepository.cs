using Thibeault.EindWerk.Objects;

namespace Thibeault.EindWerk.DataLayer
{
    public interface ICustomerRepository
    {
        Task<Customer> AddCustomerAsync();

        Task<bool> DeleteCustomerAsync(string trackingNumber);

        Task<Customer> GetCustomerByTrackingNumberAsync(string trackingNumber);

        Task<List<Customer>> GetCustomersAsync();

        Task UpdateCustomer(Customer customerToUpdate);
    }
}