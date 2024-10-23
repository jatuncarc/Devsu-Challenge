using AccountMS.Models;
using AccountMS.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace AccountMS.Repositories
{
    public class MovementRepository : IMovementRepository
    {
        private readonly AccountDbContext accountDbContext;
        public MovementRepository(AccountDbContext accountDbContext)
        {
            this.accountDbContext = accountDbContext;
        }


        public async Task<IEnumerable<Movement>> GetAll()
        {
            return await accountDbContext.Movement.ToListAsync();
        }

        public async Task<Movement> GetById(Int64 id)
        {
            return await accountDbContext.Movement.FindAsync(id);
        }

        public async Task<Movement> Add(Movement movement)
        {
            accountDbContext.Movement.Add(movement);
            await accountDbContext.SaveChangesAsync();
            return movement;
        }


        public async Task Update(Movement movement)
        {
            accountDbContext.Movement.Update(movement);
            await accountDbContext.SaveChangesAsync();
        }

        public async Task UpdatePartial(Int64 id, JsonPatchDocument<Movement> movementModel)
        {
            var movement = await accountDbContext.Movement.FindAsync(id);
            if (movement != null)
            {
                movementModel.ApplyTo(movement);
                await accountDbContext.SaveChangesAsync();
            }
        }

        public async Task Delete(Movement movement)
        {
            //Eliminacion fisica del recurso
            accountDbContext.Movement.Remove(movement);
            await accountDbContext.SaveChangesAsync();

        }


        public async Task<IEnumerable<Movement>> GetMovementsByAccountId(Int64 accountId)
        {
            return await accountDbContext.Movement
             .Where(m => m.AccountId == accountId)
             .ToListAsync();
        }
    }
}
