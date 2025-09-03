using ExperisSeguros.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExperisSeguros.Data.Models
{
    public class Poliza
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string NumeroPoliza { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal MontoPrima { get; set; }

        [Required]
        public EstadoPoliza Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        // Cliente de la póliza
        [Required]
        public string ClienteId { get; set; }
        public virtual ApplicationUser Cliente { get; set; }

        // Tipo de póliza
        [Required]
        public int TipoPolizaId { get; set; }
        public virtual TipoPoliza TipoPoliza { get; set; }
    }
}