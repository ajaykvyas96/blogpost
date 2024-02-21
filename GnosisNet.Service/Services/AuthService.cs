using GnosisNet.Data;
using GnosisNet.Entities.Entities.Identity;
using GnosisNet.Service.IServices;
using GnosisNet.Service.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnosisNet.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GnosisDbContext _context;
        private readonly ITokenManagerService _tokenManager;
        
        public AuthService(UserManager<ApplicationUser> userManager, GnosisDbContext context, ITokenManagerService tokenManager)
        {
            _userManager = userManager;
            _context = context;
            _tokenManager = tokenManager;
        }

        public Task<bool> AssignRole(string email, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequest)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequest.Email.ToLower());
            if (user == null)
            {
                return new LoginResponseDto();
            }
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!isValid)
            {
                return new LoginResponseDto();
            }
            var token = _tokenManager.GenerateToken(user);

            UserDto userDTO = new()
            {
                Email = user.Email,
                ID = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDTO,
                Token = token
            };

            return loginResponseDto;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                FirstName = registrationRequestDto.FirstName,
                LastName = registrationRequestDto.LastName
            };
            var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
            if (result.Succeeded)
            {
                var userToReturn = _context.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);
                UserDto userDto = new()
                {
                    Email = userToReturn.Email,
                    ID = userToReturn.Id,
                    FirstName = userToReturn.FirstName,
                    LastName = userToReturn.LastName
                };
                return "";
            }
            else
            {
                return result.Errors.FirstOrDefault().Description;
            }
        }
    }
}
