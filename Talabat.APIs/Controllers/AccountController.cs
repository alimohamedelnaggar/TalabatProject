using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.APIs.Error;
using Talabat.APIs.Extensions;
using Talabat.Core.Dtos;
using Talabat.Core.Dtos.Auth;
using Talabat.Core.Identity;
using Talabat.Core.Service.Contract;
using Talabat.Service.Token;

namespace Talabat.APIs.Controllers
{
    
    public class AccountController : BaseApiController
    {
        private readonly IUserService userService;
        private readonly UserManager<AppUser> userManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(IUserService userService,UserManager<AppUser> userManager,ITokenService tokenService,IMapper mapper,SignInManager<AppUser> signInManager)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
            this.signInManager = signInManager;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user=await userService.LoginAsync(loginDto);
            if (user is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));

            return Ok(user);
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user=await userService.RegisterAsync(registerDto);
            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(user);
        }
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {

           var userEmail= User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var user=await userManager.FindByEmailAsync(userEmail);
            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(new UserDto()
            {
                Email = user.Email,
                DislpayName = user.DisplayName,
                Token =await tokenService.CreateTokenAsync(user, userManager)
            });
        }
        [Authorize]
        [HttpGet("GetCurrentUserAddress")]
        public async Task<ActionResult<UserDto>> GetCurrentUserAddress()
        {

            var user=await userManager.FindByEmailWithAddressAsync(User);
            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(mapper.Map<AddressDto>(user.Address));
        }
        [HttpPut("UpdateUserAddress")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var user = await userManager.FindByEmailWithAddressAsync(User);
            if (user == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            if (user.Address == null)
            {
                user.Address = new Address();
            }

            // استخدم AutoMapper أو عدّل يدويًا
            mapper.Map(addressDto, user.Address);

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest("Problem updating the user address");

            return Ok(mapper.Map<AddressDto>(user.Address));
        }

        //[HttpGet("externallogin")]
        //public IActionResult ExternalLogin(string provider, string returnUrl = "/")
        //{
        //    var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
        //    var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        //    return Challenge(properties, provider);
        //}
        //[HttpGet("externallogincallback")]
        //public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "/")
        //{
        //    var info = await signInManager.GetExternalLoginInfoAsync();
        //    if (info == null)
        //        return BadRequest("Login info not found");

        //    var email = info.Principal.FindFirstValue(ClaimTypes.Email);

        //    if (email == null)
        //        return BadRequest("Email not found from Google");

        //    var user = await userManager.FindByEmailAsync(email);

        //    if (user == null)
        //    {
        //        user = new AppUser
        //        {
        //            UserName = email,
        //            Email = email,
        //            //FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
        //            //LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)
        //        };

        //        var createResult = await userManager.CreateAsync(user);
        //        if (!createResult.Succeeded)
        //            return BadRequest("User creation failed");

        //        await userManager.AddLoginAsync(user, info);
        //    }

        //    // ✅ إصدار JWT بدل الكوكيز
        //    var token =await tokenService.CreateTokenAsync(user,userManager);

        //    return Ok(new { token });
        //}



    }
}
