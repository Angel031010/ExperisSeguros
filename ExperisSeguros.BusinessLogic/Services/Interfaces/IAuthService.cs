using ExperisSeguros.BusinessLogic.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExperisSeguros.BusinessLogic.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        Task<bool> ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto);
        Task LogoutAsync();
    }
}
