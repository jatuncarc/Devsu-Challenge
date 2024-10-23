using AccountMS.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace AccountMS.Repositories
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> GetStatement();

    }
}
