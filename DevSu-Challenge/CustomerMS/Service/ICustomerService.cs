using CustomerMS.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace CustomerMS.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAll();

        Task<Customer> GetCustomerById(int id);

        Task<Customer> Add(Customer customer);

        Task<Customer> Update(Customer customer);

        Task UpdatePartial(int id, JsonPatchDocument<Customer> customerModel);

        Task Delete(int id);

        Task<Customer> GetCustomerByIdentification(int id);

    }
}


