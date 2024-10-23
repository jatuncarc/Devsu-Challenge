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
    [Route("cuentas")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await accountService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<Account>> AddCustomer(Account account)
        {
            try
            {
                var accountCreated = await accountService.Add(account);
                return CreatedAtAction(null, new { id = accountCreated.Number }, accountCreated);
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

        [HttpPut("{number}")]
        public async Task<ActionResult<Account>> UpdateAccount(int number, [FromBody] Account account)
        {
            try
            {
                if (number != account.Number)
                    return BadRequest("número de cuenta no coincide"); ;

                await accountService.Update(account);

                return NoContent();
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


        [HttpPatch("{number}")]
        public async Task<IActionResult> UpdatePartial(Int64 number, [FromBody] JsonPatchDocument<Account> patchAccount)
        {
            try
            {
                if (patchAccount == null)
                {
                    return BadRequest("Json vacio");
                }

                await accountService.UpdatePartial(number, patchAccount);
                return Ok();
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

        [HttpDelete("{number}")]
        public async Task<ActionResult> DeleteAccount(Int64 number)
        {
            try
            {
                await accountService.Delete(number);

                return NoContent();
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
