using CustomerMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerMS.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }


        public DbSet<Person> Person { get; set; }
        public DbSet<Customer> Customer { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");

            modelBuilder.Entity<Person>().ToTable("Person");
        }
    }
}
