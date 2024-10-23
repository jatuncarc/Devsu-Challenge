using AccountMS.Exceptions;
using AccountMS.Models;
using AccountMS.Repositories;
using Microsoft.AspNetCore.JsonPatch;

namespace AccountMS.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly ICustomerService customerService;
        public AccountService(IAccountRepository accountRepository, ICustomerService customerService)
        {
            this.accountRepository = accountRepository;
            this.customerService = customerService;
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await accountRepository.GetAll();
        }

        public async Task<Account> GetAccountByNumber(Int64 id)
        {
            return await accountRepository.GetAccountByNumber(id);
        }

        public async Task<Account> Add(Account account)
        {
            var accountExist = await GetAccountByNumber(account.Number);

            if (accountExist != null)
                throw new AccountException($"Cuenta ya existe con numero {account.Number}");


            var customer = await customerService.GetCustomerById(account.CustomerId);
            if (customer == null) throw new AccountException($"Cliente con id {account.CustomerId} no existe");


            return await accountRepository.Add(account);

        }

        public async Task<Account> Update(Account account)
        {
            var accountExist = await GetAccountByNumber(account.Number);

            if (accountExist is null)
                throw new AccountException("Cuenta no existe");

            accountExist.Number = account.Number;
            accountExist.AccountType = account.AccountType;
            accountExist.OpeningBalance = account.OpeningBalance;
            accountExist.State = account.State;
            accountExist.CustomerId = account.CustomerId;

            await accountRepository.Update(accountExist);
            return accountExist;
        }

        public async Task UpdatePartial(Int64 id, JsonPatchDocument<Account> accountModel)
        {
            var account = await GetAccountByNumber(id);

            if (account is null)
                throw new AccountException("Cuenta no existe");

            await accountRepository.UpdatePartial(id, accountModel);
        }

        public async Task Delete(Int64 id)
        {
            var account = await GetAccountByNumber(id);

            if (account is null)
                throw new AccountException("Cuenta no existe");

            await accountRepository.Delete(account);
        }

        public async Task<IEnumerable<Account>> GetAccountsByCustomerIdentification(int identification)
        {
            var customer = await customerService.GetCustomerByIdentification(identification);

            if (customer is null) throw new AccountException("Cliente no existe");

            var accounts = await accountRepository.GetAccountsByCustomerId(customer.Id);
            var accountsLenght = accounts.Count();

            if (accounts != null && accountsLenght == 0) throw new AccountException("Cliente no tiene cuentas");

            return accounts;
        }
    }
}
