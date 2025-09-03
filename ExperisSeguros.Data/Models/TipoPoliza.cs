using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExperisSeguros.Data.Models
{
    public class TipoPoliza
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [MaxLength(200)]
        public string Descripcion { get; set; }

        public virtual ICollection<Poliza> Polizas { get; set; }
    }
}