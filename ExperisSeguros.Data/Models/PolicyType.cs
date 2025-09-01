using System;
using System.Collections.Generic;
using System.Text;

namespace ExperisSeguros.Data.Models
{
    public class PolicyType
    {
        public int Id { get; set; }
        public string Nombre { get; set; } // "Vida", "Auto", "Hogar", "Salud"

        // Relaciones
        public virtual ICollection<Policy> Policies { get; set; }
    }
}
