using AccountMS.Models;
using AccountMS.Repositories;
using AccountMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AccountMS.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("reportes")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;
        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }


        [HttpGet]
        public async Task<IEnumerable<Report>> GetStatement()
        {
            return await reportService.GetStatement(10101010);
        }


    }
}
