using AccountMS.Models;
using AccountMS.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace AccountMS.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AccountDbContext accountDbContext;
        public ReportRepository(AccountDbContext accountDbContext)
        {
            this.accountDbContext = accountDbContext;
        }

        public Task<IEnumerable<Report>> GetStatement()
        {
            throw new NotImplementedException(); 
        }
    }
}
