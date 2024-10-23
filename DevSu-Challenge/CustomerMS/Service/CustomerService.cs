using CustomerMS.Data;
using CustomerMS.Exceptions;
using CustomerMS.Models;
using CustomerMS.Repositories;
using CustomerMS.Services;
using Microsoft.AspNetCore.JsonPatch;

namespace CustomerMS.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await customerRepository.GetAll();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await customerRepository.GetCustomerById(id);
        }

        public async Task<Customer> GetCustomerByIdentification(int identification)
        {
            return await customerRepository.GetCustomerByIdentification(identification);
        }

        public async Task<Customer> Add(Customer customer)
        {
            var customerExist = await GetCustomerByIdentification(customer.Identification);

            if (customerExist != null)
                throw new CustomerException($"Cliente ya existe con identificación {customer.Identification}");
            
            return await customerRepository.Add(customer);
        }

        public async Task<Customer> Update(Customer customer)
        {
            var customerExist = await GetCustomerById(customer.Id);

            if (customerExist is null)
                throw new CustomerException("Cliente no existe");

            customerExist.Name = customer.Name;
            customerExist.Gender = customer.Gender;
            customerExist.Age = customer.Age;
            customerExist.Identification = customer.Identification;
            customerExist.Address = customer.Address;
            customerExist.Phone = customer.Phone;
            customerExist.Password = customer.Password;
            customerExist.State = customer.State;

            await customerRepository.Update(customerExist);
            return customerExist;
        }

        public async Task UpdatePartial(int id, JsonPatchDocument<Customer> customerModel)
        {
            var customer = await GetCustomerById(id);

            if (customer is null)
                throw new CustomerException("Cliente no existe");

            await customerRepository.UpdatePartial(id, customerModel);
        }

        public async Task Delete(int id)
        {
            var customer = await GetCustomerById(id);

            if (customer is null)
                throw new CustomerException("Cliente no existe");

            await customerRepository.Delete(customer);
        }
    }
}
