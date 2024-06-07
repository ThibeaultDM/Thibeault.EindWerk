using Thibeault.EindWerk.Objects;

namespace Thibeault.EindWerk.DataLayer.Interfaces
{
    public interface ICustomerRepository
    {
        Task<bool> AddCustomer(Customer customerToSave);
        Task<bool> DeleteCustomer(string trackingNumber);
        Task DisposeAsync();
        Task<Customer> GetCustomerByTrackingNumber(string trackingNumber);
        Task<List<Customer>> GetCustomers();
        Task SaveAsync();
        Task UpdateCustomer(Customer customerToUpdate);
    }
}