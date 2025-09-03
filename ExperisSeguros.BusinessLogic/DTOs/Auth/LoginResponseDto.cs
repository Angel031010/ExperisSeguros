using System;
using System.Collections.Generic;
using System.Text;

namespace ExperisSeguros.BusinessLogic.DTOs.Auth
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public DateTime Expiracion { get; set; }
    }
}
