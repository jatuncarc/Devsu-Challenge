using AccountMS.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace AccountMS.Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAll();

        Task<Account> GetAccountByNumber(Int64 number);

        Task<Account> Add(Account account);

        Task Update(Account account);

        Task Delete(Account account);

        Task UpdatePartial(Int64 number, JsonPatchDocument<Account> accountModel);

        Task<IEnumerable<Account>> GetAccountsByCustomerId(int customerId);
    }
}
