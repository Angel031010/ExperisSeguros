using System;
using System.Collections.Generic;
using System.Text;

namespace ExperisSeguros.Data.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
