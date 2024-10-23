using AccountMS.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace AccountMS.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerById(int id);

        Task<Customer> GetCustomerByIdentification(int identification);

    }
}


