using System;
using System.Collections.Generic;
using System.Text;

namespace ExperisSeguros.BusinessLogic.DTOs.Poliza
{
    public class PolizaDto
    {
        public int Id { get; set; }
        public string NumeroPoliza { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal MontoPrima { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteEmail { get; set; }
        public string ClienteTelefono { get; set; }
        public string TipoPoliza { get; set; }
    }
}
