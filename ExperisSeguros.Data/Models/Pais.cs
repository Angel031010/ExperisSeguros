using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExperisSeguros.Data.Models
{
    public class Pais
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(3)]
        public string Codigo { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        public virtual ICollection<ApplicationUser> Usuarios { get; set; }
    }
}