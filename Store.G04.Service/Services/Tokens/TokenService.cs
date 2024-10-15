using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Store.G04.Core.Entities.Identity;
using Store.G04.Core.ServicesContract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
namespace Store.G04.Service.Services.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration) {
            _configuration = configuration;
        }


        public async Task<string> CreateTokenAsync(AppUser user,UserManager<AppUser> manager )
        {
            //1- Header (algo,Type)
            //2- Playload (claims)
            //3- Signature 
            var authClaims = new List<Claim>() {
             new Claim(ClaimTypes.Email,user.Email),
             new Claim(ClaimTypes.GivenName,user.DisplayName),
             new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
            


            };
            var userRole = await manager.GetRolesAsync(user);
            foreach (var role in userRole) {
                authClaims.Add(new Claim( ClaimTypes.Role,role));
            
            
            
            }
            var authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            


            var token = new JwtSecurityToken(
                  issuer: _configuration["Jwt:Issuer"],
                  audience: _configuration["Jwt:Audience"],
                  expires: DateTime.Now.AddDays(double.Parse(_configuration["Jwt:DurationInDays"])),
                  claims :authClaims,
                  signingCredentials:new SigningCredentials(authkey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
