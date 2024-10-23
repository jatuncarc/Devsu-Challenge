using AccountMS.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace AccountMS.Services
{
    public interface IMovementService
    {
        Task<IEnumerable<Movement>> GetAll();
        
        Task<Movement> GetMovementById(Int64 id);

        Task<Movement> Add(Movement movement);

        Task<Movement> Update(Movement movement);

        Task UpdatePartial(Int64 id, JsonPatchDocument<Movement> movementModel);

        Task Delete(Int64 id);

        Task<IEnumerable<Movement>> GetMovementsByAccountId(Int64 accountId);
    }
}


