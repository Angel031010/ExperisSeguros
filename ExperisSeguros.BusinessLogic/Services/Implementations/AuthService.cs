using ExperisSeguros.BusinessLogic.DTOs.Auth;
using ExperisSeguros.BusinessLogic.Services.Interfaces;
using ExperisSeguros.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExperisSeguros.BusinessLogic.Services.Implementations
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                throw new InvalidOperationException("Credenciales inválidas");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                throw new InvalidOperationException("Credenciales inválidas");

            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user, roles.FirstOrDefault());

            return new LoginResponseDto
            {
                Token = token,
                Email = user.Email,
                Nombre = $"{user.Nombre} {user.ApellidoPaterno}",
                Rol = roles.FirstOrDefault(),
                Expiracion = DateTime.UtcNow.AddHours(1)
            };
        }

        public async Task<bool> ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new InvalidOperationException("Usuario no encontrado");

            var result = await _userManager.ChangePasswordAsync(
                user,
                changePasswordDto.PasswordActual,
                changePasswordDto.PasswordNuevo);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Error al cambiar contraseña: {errors}");
            }

            return true;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        private string GenerateJwtToken(ApplicationUser user, string role)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
