using AccountMS.Models;
using AccountMS.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace AccountMS.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDbContext accountDbContext;
        public AccountRepository(AccountDbContext accountDbContext)
        {
            this.accountDbContext = accountDbContext;
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await accountDbContext.Account.ToListAsync();
        }

        public async Task<Account> Add(Account account)
        {
            accountDbContext.Account.Add(account);
            await accountDbContext.SaveChangesAsync();
            return account;
        }

        public async Task<Account> GetAccountByNumber(Int64 number)
        {
            return await accountDbContext.Account.FindAsync(number);
        }

        public async Task Update(Account account)
        {
            accountDbContext.Account.Update(account);
            await accountDbContext.SaveChangesAsync();
        }

        public async Task UpdatePartial(Int64 number, JsonPatchDocument<Account> accountModel)
        {
            var account = await accountDbContext.Account.FindAsync(number);
            if (account != null)
            {
                accountModel.ApplyTo(account);
                await accountDbContext.SaveChangesAsync();
            }
        }

        public async Task Delete(Account account)
        {

            //Eliminacion fisica del recurso
            //cuentaDbContext.Cuenta.Remove(cuenta);

            account.State = false;
            await accountDbContext.SaveChangesAsync();

        }


        public async Task<IEnumerable<Account>> GetAccountsByCustomerId(int customerId)
        {
            var accounts = await accountDbContext.Account
             .Where(m => m.CustomerId == customerId)
             .ToListAsync();

            return accounts;
        }
    }
}
