using AccountMS.Models;
using AccountMS.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AccountMS.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository reportRepository;
        private readonly IMovementService movementService;
        private readonly IAccountService accountService;
        private readonly ICustomerService customerService;

        public ReportService(IReportRepository reportRepository,
            IMovementService movementService,
            IAccountService accountService,
            ICustomerService customerService)
        { 
            this.reportRepository = reportRepository;
            this.movementService = movementService;
            this.accountService = accountService;
            this.customerService = customerService;
        }

        public async Task<IEnumerable<Report>> GetStatement(int CustomerIdentication)
        {
            
            var accounts = await accountService.GetAccountsByCustomerIdentification(CustomerIdentication);
            var customer = await customerService.GetCustomerByIdentification(CustomerIdentication);

            List<Report> report = new List<Report>();

            foreach (var account in accounts)
            {
                var movements = await movementService.GetMovementsByAccountId(account.Number);

                foreach(var movement in movements) {

                    report.Add(new Report
                    {
                        Fecha = DateTime.Now,
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

            //var query = from m in movements
            //            join a in _context.Cuentas on m.AccountId equals a.Number
            //            join p in _context.Personas on a.CustomerId equals p.Id
            //            select new
            //            {
            //                FechaMovimiento = m.Date,
            //                NombreCliente = p.Name,
            //                NumeroCuenta = a.Number,
            //                TipoCuenta = a.AccountType,
            //                SaldoInicial = a.OpeningBalance,
            //                EstadoCuenta = a.State,
            //                ValorMovimiento = m.Value,
            //                SaldoDespuesMovimiento = m.Balance
            //            };

            //return query.ToList();

            return report;
        }

    }
}
