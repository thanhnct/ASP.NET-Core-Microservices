using Customer.API.Models;
using Shared.DTOs.Customer;
using Shared.DTOs;

namespace Customer.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<PagedDto<CustomerDto>> GetCustomers(int page = 1);

        Task<CustomerDto?> GetCustomerById(int id);

        Task<int> CreateCustomer(CustomerCreateDto CustomerDto);

        Task UpdateCustomer(int id, CustomerUpdateDto CustomerDto);

        Task DeleteCustomer(int id);
    }
}
