using ExperisSeguros.BusinessLogic.DTOs.Poliza;
using ExperisSeguros.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExperisSeguros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PolizasController : ControllerBase
    {
        private readonly IPolizaService _polizaService;

        public PolizasController(IPolizaService polizaService)
        {
            _polizaService = polizaService;
        }

        [HttpPost("cotizar")]
        public async Task<IActionResult> CotizarPoliza([FromBody] CrearPolizaDto crearPolizaDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var poliza = await _polizaService.CotizarPolizaAsync(crearPolizaDto, userId);
                return CreatedAtAction(nameof(GetPoliza), new { id = poliza.Id }, poliza);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPolizas()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var polizas = await _polizaService.ObtenerPolizasAsync(userId);
                return Ok(polizas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPoliza(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var poliza = await _polizaService.ObtenerPolizaDetalleAsync(id, userId);
                return Ok(poliza);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarPoliza(int id, [FromBody] ActualizarPolizaDto actualizarDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var poliza = await _polizaService.ActualizarPolizaAsync(id, actualizarDto, userId);
                return Ok(poliza);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/autorizar")]
        [Authorize(Roles = "Admin,Broker")]
        public async Task<IActionResult> AutorizarPoliza(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await _polizaService.AutorizarPolizaAsync(id, userId);

                if (result)
                    return Ok(new { message = "Póliza autorizada exitosamente" });

                return BadRequest(new { message = "Error al autorizar la póliza" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/rechazar")]
        [Authorize(Roles = "Admin,Broker")]
        public async Task<IActionResult> RechazarPoliza(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await _polizaService.RechazarPolizaAsync(id, userId);

                if (result)
                    return Ok(new { message = "Póliza rechazada exitosamente" });

                return BadRequest(new { message = "Error al rechazar la póliza" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}