using AccountMS.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountMS.Data
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options) { }


        public DbSet<Account> Account { get; set; }
        public DbSet<Movement> Movement { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Movement>().ToTable("Movement");
        }
    }
}
