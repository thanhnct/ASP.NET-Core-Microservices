using Contracts.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Customer.API.Data;
using Customer.API.Models;
using Customer.API.Services.Interfaces;
using Shared.DTOs.Customer;
using Shared.DTOs;
using X.PagedList.EF;


namespace Customer.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepositoryBaseAsync<Models.Customer, int, CustomerContext> _customerRepository;

        public CustomerService(IRepositoryBaseAsync<Models.Customer, int, CustomerContext> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<PagedDto<CustomerDto>> GetCustomers(int page = 1)
        {
            var Customers = await _customerRepository.FindAll().ToPagedListAsync(page, 10);

            return new PagedDto<CustomerDto>
            {
                Items = Customers.Select(p => new CustomerDto
                {
                    Id = p.Id,
                    UserName = p.UserName,
                    LastName = p.LastName,
                    FirstName = p.FirstName,
                    Email = p.Email,
                }),
                Page = Customers.PageNumber,
                PageSize = Customers.PageSize,
                TotalCount = Customers.TotalItemCount
            };
        }

        public async Task<CustomerDto?> GetCustomerById(int id)
        {
            var Customer = await _customerRepository.FindByCondition(p => p.Id == id).Select(p => new CustomerDto
            {
                Id = p.Id,
                UserName = p.UserName,
                LastName = p.LastName,
                FirstName = p.FirstName,
                Email = p.Email,
            }).FirstOrDefaultAsync();

            return Customer;
        }

        public async Task<int> CreateCustomer(CustomerCreateDto dto)
        {
            var newCustomer = new Models.Customer
            {
                Id = 0,
                UserName = dto.UserName,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                Email = dto.Email,
            };

            var id = await _customerRepository.CreateAsync(newCustomer);
            return id;
        }

        public async Task UpdateCustomer(int id, CustomerUpdateDto dto)
        {
            var customer = await _customerRepository.FindByCondition(p => p.Id == id).FirstOrDefaultAsync();
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }
            customer.UserName = dto.UserName;
            customer.LastName = dto.LastName;
            customer.FirstName = dto.FirstName;
            customer.Email = dto.Email;
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteCustomer(int id)
        {
            var Customer = await _customerRepository.FindByCondition(p => p.Id == id).FirstOrDefaultAsync();
            if (Customer == null)
            {
                throw new Exception("Customer not found");
            }
            await _customerRepository.DeleteAsync(Customer);
        }
    }
}
