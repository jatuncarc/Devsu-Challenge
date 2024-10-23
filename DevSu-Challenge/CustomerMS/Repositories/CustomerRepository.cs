using CustomerMS.Data;
using CustomerMS.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CustomerMS.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext customerDbContext;

        public CustomerRepository(CustomerDbContext customerDbContext)
        {
            this.customerDbContext = customerDbContext;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await customerDbContext.Customer.ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await customerDbContext.Customer.FindAsync(id);
        }

        public async Task<Customer> Add(Customer customer)
        {
            customerDbContext.Customer.Add(customer);
            await customerDbContext.SaveChangesAsync();
            return customer;
        }

        public async Task Update(Customer customer)
        {
            customerDbContext.Customer.Update(customer);
            await customerDbContext.SaveChangesAsync();
        }

        public async Task UpdatePartial(int id, JsonPatchDocument<Customer> customerModel)
        {
            var customer = await customerDbContext.Customer.FindAsync(id);

            customerModel.ApplyTo(customer);
            await customerDbContext.SaveChangesAsync();
        }
        public async Task Delete(Customer customer)
        {
            customerDbContext.Customer.Remove(customer);
            await customerDbContext.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomerByIdentification(int identification)
        {
            return await customerDbContext.Customer
             .Where(m => m.Identification == identification).FirstOrDefaultAsync();

        }
    }
}
