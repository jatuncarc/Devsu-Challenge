using CustomerMS.Exceptions;
using CustomerMS.Models;
using CustomerMS.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CustomerMS.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("clientes")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await customerService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var customer = await customerService.GetCustomerById(id);
            return Ok(customer);
        }

        [HttpGet("GetCustomerByIdentification/{id}")]
        public async Task<ActionResult<Customer>> GetCustomerByIdentification(int id)
        {
            var customer = await customerService.GetCustomerByIdentification(id);
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        {
            try
            {
                var customerCreated = await customerService.Add(customer);
                return CreatedAtAction(null, new { id = customerCreated.Id }, customerCreated);
            }
            catch (CustomerException ex)
            {
                return BadRequest(new { message = ex.Message });
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error inesperado.", detalle = ex.Message });
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            try
            {
                if (id != customer.Id)
                    return BadRequest("Id de cliente no coincide");

                await customerService.Update(customer);

                return NoContent();
            }
            catch (CustomerException ex)
            {
                return BadRequest(new { message = ex.Message });
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error inesperado.", detalle = ex.Message });
            }

        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePartialCustomer(int id, [FromBody] JsonPatchDocument<Customer> patchCustomer)
        {
            if (patchCustomer == null)
            {
                return BadRequest("Json vacio");
            }


            await customerService.UpdatePartial(id, patchCustomer);
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            try
            {
                await customerService.Delete(id);

                return NoContent();
            }
            catch (CustomerException ex)
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
