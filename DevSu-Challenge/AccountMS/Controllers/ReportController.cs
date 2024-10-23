using AccountMS.Exceptions;
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
        public async Task<ActionResult<IEnumerable<Report>>> GetStatement([FromQuery] int cliente, [FromQuery] DateTime? fechaDesde, [FromQuery] DateTime? fechaHasta)
        {
            try
            {
                return Ok(await reportService.GetStatement(cliente, fechaDesde, fechaHasta));
            }
            catch (AccountException ex)
            {
                return BadRequest(new { message = ex.Message });
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error inesperado.", detalle = ex.Message });
            }

        }


    }
}
