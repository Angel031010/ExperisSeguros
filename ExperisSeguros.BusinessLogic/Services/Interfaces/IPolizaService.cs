using ExperisSeguros.BusinessLogic.DTOs.Poliza;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExperisSeguros.BusinessLogic.Services.Interfaces
{
    public interface IPolizaService
    {
        Task<PolizaDto> CotizarPolizaAsync(CrearPolizaDto crearPolizaDto, string userId);
        Task<PolizaDto> ActualizarPolizaAsync(int polizaId, ActualizarPolizaDto actualizarDto, string userId);
        Task<bool> AutorizarPolizaAsync(int polizaId, string userId);
        Task<bool> RechazarPolizaAsync(int polizaId, string userId);
        Task<IEnumerable<PolizaDto>> ObtenerPolizasAsync(string userId);
        Task<PolizaDto> ObtenerPolizaDetalleAsync(int polizaId, string userId);
    }

}
