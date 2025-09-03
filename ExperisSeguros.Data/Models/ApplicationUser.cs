using ExperisSeguros.Data.Enums;
using ExperisSeguros.Data.Validations;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExperisSeguros.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        public string ApellidoPaterno { get; set; }

        [MaxLength(100)]
        public string ApellidoMaterno { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [EdadMinima(18)]
        public DateTime FechaNacimiento { get; set; }

        [NotMapped]
        public int Edad
        {
            get
            {
                var hoy = DateTime.Today;
                var edad = hoy.Year - FechaNacimiento.Year;

                if (FechaNacimiento.Date > hoy.AddYears(-edad))
                    edad--;

                return edad;
            }
        }

        [Required]
        public Genero Genero { get; set; }

        [Required]
        public int PaisId { get; set; }
        public virtual Pais Pais { get; set; }

        // Pólizas donde es el cliente
        public virtual ICollection<Poliza> PolizasComoCliente { get; set; }
    }
}
