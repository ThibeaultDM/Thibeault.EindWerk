﻿using Thibeault.EindWerk.Objects;

namespace Thibeault.EindWerk.DataLayer
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Creates a customer in the database with trackingNumber "K0" and returns an empty customer who you can assign values to.
        /// Use it to ensure no duplicate id's in the database.
        /// </summary>
        /// <returns>An empty customer whom you can update to assign values</returns>
        Task<Customer> CreateCustomerAsync();

        Task<bool> DeleteCustomerAsync(string trackingNumber);

        Task<Customer> GetCustomerByTrackingNumberAsync(string trackingNumber);

        Task<List<Customer>> GetCustomersAsync();

        Task UpdateCustomer(Customer customerToUpdate);
    }
}