using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExperisSeguros.BusinessLogic.DTOs.Auth
{
    public class ChangePasswordDto
    {
        [Required]
        public string PasswordActual { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string PasswordNuevo { get; set; }

        [Required]
        [Compare("PasswordNuevo", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarPassword { get; set; }
    }
}
