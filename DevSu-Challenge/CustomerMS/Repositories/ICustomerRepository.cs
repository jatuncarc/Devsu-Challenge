using CustomerMS.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace CustomerMS.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();

        Task<Customer> GetCustomerById(int id);

        Task<Customer> Add(Customer customer);

        Task Update(Customer customer);

        Task UpdatePartial(int id, JsonPatchDocument<Customer> customerModel);

        Task Delete(Customer customer);

        Task<Customer> GetCustomerByIdentification(int identification);

    }
}
