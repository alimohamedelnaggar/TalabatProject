using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos;
using Talabat.Core.Dtos.Auth;
using Talabat.Core.Identity;
using Talabat.Core.Service.Contract;

namespace Talabat.Service.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService token;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,ITokenService token)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.token = token;
        }

        public async Task<bool> CheckEmailExitsAsync(string email)
        {
           return await userManager.FindByEmailAsync(email) is not null;
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return null;
            var result= await signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);
            if (!result.Succeeded) return null;
            return new UserDto()
            {
                DislpayName = user.DisplayName,
                Email = loginDto.Email,
                Token =await token.CreateTokenAsync(user, userManager)
            };
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            if (await CheckEmailExitsAsync(registerDto.Email)) return null;
            var user = new AppUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email.Split("@")[0]
            };
            var result = await userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded) return null;
            return new UserDto()
            {
                Email = registerDto.Email,
                DislpayName = registerDto.DisplayName,
                Token = await token.CreateTokenAsync(user, userManager)
            };
        }
    }
}
