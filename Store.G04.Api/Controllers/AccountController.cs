using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Api.Errors;
using Store.G04.Api.Extentions;
using Store.G04.Core.Dtos.AuthDto;
using Store.G04.Core.Entities.Identity;
using Store.G04.Core.ServicesContract;
using Store.G04.Service.Services.Tokens;
using System.Security.Claims;

namespace Store.G04.Api.Controllers
{
  
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, UserManager<AppUser> userManager, ITokenService tokenService,IMapper mapper)
        {
        
          _userService = userService;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]// /api/Account/login
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
          var user=await  _userService.LoginAsync(login);

            if (user is null) { return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized)); }

             return Ok(user);
        
        }
           [HttpPost("register")]// /api/Account/login
        public async Task<ActionResult<RegisterDto>> Register(RegisterDto register)
        {
          var user=await  _userService.RegisterAsync(register);

            if (user is null) { return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest,"invald Registration !!")); }

             return Ok(user);
        
        }
        [HttpGet("GetCurrentUser")]
        [Authorize]

        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if(userEmail is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });

        } 
        [HttpGet("GetCurrentAddress")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentAddress()
        {
            
            var user = await _userManager.FindEmailWithAddressAsync(User);
            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(_mapper.Map<AddressDto>(user.Address));
        }
        [HttpPost("UpdateAddress")]
        [Authorize]
        public async Task<ActionResult<UserDto>> UpdateAddress(AddressDto address)
        {
            var user = await _userManager.FindEmailWithAddressAsync(User);
            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
                 
            if(user.Address == null)
            {
                user.Address = new Address();

            }
            user.Address.FName = address.FName;
            user.Address.LName = address.LName;
            user.Address.City = address.City;
            user.Address.Country = address.Country;
            user.Address.Streat = address.Streat;

            var result=await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest,"Error While Updating Address"));
            }
            return Ok(_mapper.Map<AddressDto>(user.Address));



        }


    }
}
