using AccountMS.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace AccountMS.Services
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetStatement(int CustomerIdentification, DateTime? fechaDesde, DateTime? fechaHasta);
    }
}