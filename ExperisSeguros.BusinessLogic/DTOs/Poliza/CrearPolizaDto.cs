using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExperisSeguros.BusinessLogic.DTOs.Poliza
{
    public class CrearPolizaDto
    {
        [Required]
        public int TipoPolizaId { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        public string ClienteId { get; set; }
    }
}
