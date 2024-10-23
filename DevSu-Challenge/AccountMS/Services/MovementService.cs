using AccountMS.Exceptions;
using AccountMS.Models;
using AccountMS.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AccountMS.Services
{
    public class MovementService : IMovementService
    {
        private static readonly string DepositCode = "D";
        private static readonly string ChargeCode = "R";

        private readonly IMovementRepository movementRepository;
        private readonly IAccountService accountService;
        public MovementService(IMovementRepository movementRepository,
                                IAccountService accountService)
        {
            this.movementRepository = movementRepository;
            this.accountService = accountService;
        }

        public async Task<IEnumerable<Movement>> GetAll()
        {
            return await movementRepository.GetAll();
        }

        public async Task<Movement> GetMovementById(Int64 id)
        {
           return await movementRepository.GetById(id);
        }

        public async Task<Movement> Add(Movement movement)
        {

            string[] movementTypesAllow = new string[] { DepositCode, ChargeCode };
            if (!movementTypesAllow.Contains(movement.MovementType))
                throw new AccountException("Valores permitidos para MovementType: [D]=Depósito  [R]=Retiro");

            if (movement.Value == 0)
            {
                throw new AccountException("Monto debe ser distinto de 0");
            }
            else
            {
                if (movement.MovementType == DepositCode && movement.Value < 0)
                    throw new AccountException("Monto para depósitos debe ser positivo");
                if (movement.MovementType == ChargeCode && movement.Value > 0)
                    throw new AccountException("Monto para retiros debe ser negativo");
            }

            //Obtener los datos de la cuenta para validar sus datos
            var account = await accountService.GetAccountByNumber(movement.AccountId);

            if (account is null)
            {
                throw new AccountException("Cuenta no existe.");
            }
            else
            {
                if (!account.State)
                    throw new AccountException("Cuenta no esta activa.");
            }


            //Consultar si tiene movimientos para tomar el ultimo saldo
            var currentMovements = await GetMovementsByAccountId(movement.AccountId);
            var lengthMovements = currentMovements.Count();

            if (currentMovements != null && lengthMovements > 0)
            {
                //Obtener el ultimo movimiento
                var lastMovement = currentMovements.OrderByDescending(m => m.Id).FirstOrDefault();
                movement.Balance = availableBalance(movement, lastMovement.Balance);
            }
            else
            { // Obtener el saldo inicial de la cuenta
                movement.Balance = availableBalance(movement, account.OpeningBalance);
            }

            var movementCreated = await movementRepository.Add(movement);
            return movementCreated;
        }

        public async Task<Movement> Update(Movement movement)
        {
            var movementExist = await GetMovementById(movement.Id);

            if (movementExist is null)
                throw new AccountException("Movimiento no existe");

            movementExist.Id = movement.Id;
            movementExist.Date = movement.Date;
            movementExist.MovementType = movement.MovementType;
            movementExist.Value = movement.Value;
            movementExist.Balance = movement.Balance;
            movementExist.AccountId = movement.AccountId;

            await movementRepository.Update(movementExist);
            return movementExist;
        }

        public async Task UpdatePartial(long id, JsonPatchDocument<Movement> movementModel)
        {
            var movement = await GetMovementById(id);

            if (movement is null)
                throw new AccountException("Movimiento no existe");

            await movementRepository.UpdatePartial(id, movementModel);
        }

        public async Task Delete(long id)
        {
            var movement = await GetMovementById(id);

            if (movement is null)
                throw new AccountException("Movimiento no existe");

            await movementRepository.Delete(movement);
        }


        public async Task<IEnumerable<Movement>> GetMovementsByAccountId(Int64 accountId)
        {
            return await movementRepository.GetMovementsByAccountId(accountId);
        }

        private decimal availableBalance(Movement movement, decimal Balance)
        {

            if (movement.MovementType == ChargeCode)
            {
                if (Balance >= movement.Value * -1)
                {
                    return Balance + movement.Value;
                }
                else
                {
                    throw new AccountException("Cuenta no tiene saldo suficiente");
                }
            }
            else
            {
                return Balance + movement.Value;
            }
        }
    }
}
