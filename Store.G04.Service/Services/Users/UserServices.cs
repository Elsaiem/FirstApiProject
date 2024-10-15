using Microsoft.AspNetCore.Identity;
using Store.G04.Core.Dtos.AuthDto;
using Store.G04.Core.Entities.Identity;
using Store.G04.Core.ServicesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Service.Services.Users
{
    public class UserServices : IUserService
    {
        private readonly UserManager<AppUser> _UserManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserServices(UserManager<AppUser> userManager
               , SignInManager<AppUser> signInManager
               ,ITokenService tokenService )
        {
            _UserManager = userManager;
            this._signInManager = signInManager;
            this._tokenService = tokenService;
        }

       

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
         var user=await _UserManager.FindByEmailAsync(loginDto.Email);
            if (user == null) { return null; }
          var result= await  _signInManager.CheckPasswordSignInAsync(user,loginDto.Password, false);
            if (!result.Succeeded) {
                return null; 
            
            }
            return new UserDto(){
                DisplayName=user.DisplayName,
                Email=user.Email,
                Token=await _tokenService.CreateTokenAsync(user,_UserManager)
            };
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            if (! await CheckEmailExistAsync(registerDto.Email))
            {
                return null;
            }
            var user = new AppUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email.Split("@")[0],
            };
         var result= await  _UserManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) {
                return null;
            
            }
            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _UserManager)
            };
        }

        public async Task<bool> CheckEmailExistAsync(string email)
        {
            return await _UserManager.FindByEmailAsync(email) is null;


        }


    }
}
