using AccountMS.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace AccountMS.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAll();
        
        Task<Account> GetAccountByNumber(Int64 id);

        Task<Account> Add(Account account);

        Task<Account> Update(Account account);

        Task UpdatePartial(Int64 id, JsonPatchDocument<Account> accountModel);

        Task Delete(Int64 id);


        Task<IEnumerable<Account>> GetAccountsByCustomerIdentification(int identification);

    }
}


