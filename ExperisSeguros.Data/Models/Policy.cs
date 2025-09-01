using ExperisSeguros.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ExperisSeguros.Data.Models
{
    public class Policy
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string NumeroPoliza { get; set; }

        public int PolicyTypeId { get; set; }
        public virtual PolicyType PolicyType { get; set; }

        public string ClientId { get; set; }
        public virtual ApplicationUser Client { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoPrima { get; set; }

        public PolicyStatus Status { get; set; }

        public string BrokerId { get; set; }
        public virtual ApplicationUser Broker { get; set; }
    }
}
