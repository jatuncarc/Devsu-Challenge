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
    [Route("movimientos")]
    public class MovementController : ControllerBase
    {
        private readonly IMovementService movementService;

        public MovementController(IMovementService movementService)
        {
            this.movementService = movementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await movementService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<Movement>> Add(Movement movement)
        {
            try
            {
                var movementCreated = await movementService.Add(movement);

                return CreatedAtAction(null, new { id = movementCreated.Id }, movementCreated);
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

        [HttpPut("{id}")]
        public async Task<ActionResult<Movement>> Update(Int64 id, [FromBody] Movement movement)
        {
            try
            {
                if (id != movement.Id)
                    return BadRequest("número de movimiento no coincide");

                await movementService.Update(movement);

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


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePartial(Int64 id, [FromBody] JsonPatchDocument<Movement> patchMovement)
        {
            try
            {
                if (patchMovement == null)
                {
                    return BadRequest("Json vacio");
                }

                await movementService.UpdatePartial(id, patchMovement);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Int64 id)
        {
            try
            {
                await movementService.Delete(id);

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
