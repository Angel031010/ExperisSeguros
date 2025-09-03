using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExperisSeguros.BusinessLogic.DTOs.Poliza
{
    public class ActualizarPolizaDto
    {
        [Required]
        public int TipoPolizaId { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal MontoPrima { get; set; }
    }
}
