using ExperisSeguros.BusinessLogic.DTOs.Poliza;
using ExperisSeguros.BusinessLogic.Helpers;
using ExperisSeguros.BusinessLogic.Services.Interfaces;
using ExperisSeguros.Data;
using ExperisSeguros.Data.Enums;
using ExperisSeguros.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperisSeguros.BusinessLogic.Services.Implementations
{
    public class PolizaService : IPolizaService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PolizaService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<PolizaDto> CotizarPolizaAsync(CrearPolizaDto crearPolizaDto, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            // Determinar cliente
            ApplicationUser cliente;
            if (roles.Contains("Cliente"))
            {
                cliente = user;
            }
            else if (roles.Contains("Broker") || roles.Contains("Admin"))
            {
                if (string.IsNullOrEmpty(crearPolizaDto.ClienteId))
                    throw new InvalidOperationException("Debe especificar un cliente");

                cliente = await _userManager.FindByIdAsync(crearPolizaDto.ClienteId);
            }
            else
            {
                throw new UnauthorizedAccessException("No tiene permisos para cotizar pólizas");
            }


            if (cliente.Edad < 18)
                throw new InvalidOperationException("El cliente debe ser mayor de 18 años");

            decimal montoPrima = cliente.Genero == Genero.Mujer ? 2500.00m : 2000.00m;

            var poliza = new Poliza
            {
                NumeroPoliza = GeneradorNumeroPoliza.Generar(),
                FechaInicio = crearPolizaDto.FechaInicio,
                FechaFin = crearPolizaDto.FechaInicio.AddMonths(1),
                MontoPrima = montoPrima,
                Estado = EstadoPoliza.Cotizada,
                ClienteId = cliente.Id,
                TipoPolizaId = crearPolizaDto.TipoPolizaId,
                FechaCreacion = DateTime.Now
            };

            _context.Polizas.Add(poliza);
            await _context.SaveChangesAsync();

            return await MapToPolizaDto(poliza);
        }

        public async Task<PolizaDto> ActualizarPolizaAsync(int polizaId, ActualizarPolizaDto actualizarDto, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            var poliza = await _context.Polizas
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(p => p.Id == polizaId);

            if (poliza == null)
                throw new InvalidOperationException("Póliza no encontrada");

            if (poliza.Estado != EstadoPoliza.Cotizada)
                throw new InvalidOperationException("Solo se pueden modificar pólizas en estado Cotizada");

            if (roles.Contains("Cliente") && poliza.ClienteId != userId)
                throw new UnauthorizedAccessException("No tiene permisos para modificar esta póliza");

            poliza.TipoPolizaId = actualizarDto.TipoPolizaId;
            poliza.FechaInicio = actualizarDto.FechaInicio;
            poliza.FechaFin = actualizarDto.FechaInicio.AddMonths(1);
            poliza.MontoPrima = actualizarDto.MontoPrima;

            await _context.SaveChangesAsync();
            return await MapToPolizaDto(poliza);
        }

        public async Task<bool> AutorizarPolizaAsync(int polizaId, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains("Admin") && !roles.Contains("Broker"))
                throw new UnauthorizedAccessException("No tiene permisos para autorizar pólizas");

            var poliza = await _context.Polizas.FindAsync(polizaId);

            if (poliza == null)
                throw new InvalidOperationException("Póliza no encontrada");

            if (poliza.Estado != EstadoPoliza.Cotizada)
                throw new InvalidOperationException("Solo se pueden autorizar pólizas cotizadas");

            poliza.Estado = EstadoPoliza.Autorizada;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RechazarPolizaAsync(int polizaId, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains("Admin") && !roles.Contains("Broker"))
                throw new UnauthorizedAccessException("No tiene permisos para rechazar pólizas");

            var poliza = await _context.Polizas.FindAsync(polizaId);

            if (poliza == null)
                throw new InvalidOperationException("Póliza no encontrada");

            if (poliza.Estado != EstadoPoliza.Cotizada)
                throw new InvalidOperationException("Solo se pueden rechazar pólizas cotizadas");

            poliza.Estado = EstadoPoliza.Rechazada;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<PolizaDto>> ObtenerPolizasAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            IQueryable<Poliza> query = _context.Polizas
                .Include(p => p.Cliente)
                .Include(p => p.TipoPoliza);

            if (roles.Contains("Cliente"))
            {
                query = query.Where(p => p.ClienteId == userId);
            }

            var polizas = await query.ToListAsync();
            var polizasDto = new List<PolizaDto>();

            foreach (var poliza in polizas)
            {
                polizasDto.Add(await MapToPolizaDto(poliza));
            }

            return polizasDto;
        }

        public async Task<PolizaDto> ObtenerPolizaDetalleAsync(int polizaId, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            var poliza = await _context.Polizas
                .Include(p => p.Cliente)
                .Include(p => p.TipoPoliza)
                .FirstOrDefaultAsync(p => p.Id == polizaId);

            if (poliza == null)
                throw new InvalidOperationException("Póliza no encontrada");

            if (roles.Contains("Cliente") && poliza.ClienteId != userId)
                throw new UnauthorizedAccessException("No tiene permisos para ver esta póliza");

            return await MapToPolizaDto(poliza);
        }

        private async Task<PolizaDto> MapToPolizaDto(Poliza poliza)
        {
            // Cargar cliente si no está cargado
            if (poliza.Cliente == null)
            {
                poliza = await _context.Polizas
                    .Include(p => p.Cliente)
                    .Include(p => p.TipoPoliza)
                    .FirstAsync(p => p.Id == poliza.Id);
            }

            return new PolizaDto
            {
                Id = poliza.Id,
                NumeroPoliza = poliza.NumeroPoliza,
                FechaInicio = poliza.FechaInicio,
                FechaFin = poliza.FechaFin,
                MontoPrima = poliza.MontoPrima,
                Estado = poliza.Estado.ToString(),
                FechaCreacion = poliza.FechaCreacion,
                ClienteNombre = $"{poliza.Cliente.Nombre} {poliza.Cliente.ApellidoPaterno} {poliza.Cliente.ApellidoMaterno}",
                ClienteEmail = poliza.Cliente.Email,
                ClienteTelefono = poliza.Cliente.PhoneNumber,
                TipoPoliza = poliza.TipoPoliza?.Nombre
            };
        }
    }
}
