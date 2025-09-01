using ExperisSeguros.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ExperisSeguros.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int Edad { get; set; }
        public Genero Genero { get; set; }
        public string Role { get; set; } // "Admin", "Broker", "Cliente"

        public virtual ICollection<Policy> Policies { get; set; }
    }
}
