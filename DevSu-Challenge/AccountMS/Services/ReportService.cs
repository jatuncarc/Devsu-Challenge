using AccountMS.Models;
using AccountMS.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AccountMS.Services
{
    public class ReportService : IReportService
    {
        private readonly IMovementService movementService;
        private readonly IAccountService accountService;
        private readonly ICustomerService customerService;

        public ReportService(IMovementService movementService,
            IAccountService accountService,
            ICustomerService customerService)
        {
            this.movementService = movementService;
            this.accountService = accountService;
            this.customerService = customerService;
        }

        public async Task<IEnumerable<Report>> GetStatement(int CustomerIdentification, DateTime? fechaDesde, DateTime? fechaHasta)
        {

            var accounts = await accountService.GetAccountsByCustomerIdentification(CustomerIdentification);
            var customer = await customerService.GetCustomerByIdentification(CustomerIdentification);

            List<Report> report = new List<Report>();

            foreach (var account in accounts)
            {
                var movements = await movementService.GetMovementsByAccountId(account.Number);

                var filteredMovements = movements.Where(x =>
                                        (!fechaDesde.HasValue || x.Date >= fechaDesde) &&
                                        (!fechaHasta.HasValue || x.Date <= fechaHasta)).OrderByDescending(m => m.Id);

                foreach (var movement in filteredMovements)
                {

                    report.Add(new Report
                    {
                        Fecha = movement.Date,
                        Cliente = customer.Name,
                        NumeroCuenta = movement.AccountId,
                        Tipo = account.AccountType,
                        SaldoInicial = account.OpeningBalance,
                        Estado = account.State,
                        Movimiento = movement.Value,
                        SaldoDisponible = movement.Balance
                    });
                }


            }

            return report;
        }

    }
}
