using AccountMS.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace AccountMS.Repositories
{
    public interface IMovementRepository
    {
        Task<IEnumerable<Movement>> GetAll();

        Task<Movement> GetById(Int64 id);

        Task<Movement> Add(Movement movement);

        Task Update(Movement movement);

        Task UpdatePartial(Int64 id, JsonPatchDocument<Movement> movementModel);

        Task Delete(Movement movement);

        Task<IEnumerable<Movement>> GetMovementsByAccountId(Int64 accountId);
    }
}
