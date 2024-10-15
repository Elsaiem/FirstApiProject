using Microsoft.AspNetCore.Identity;
using Store.G04.Core.Dtos.AuthDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.ServicesContract
{
    public interface IUserService
    {
     Task<UserDto>   LoginAsync(LoginDto loginDto);
     Task<UserDto>   RegisterAsync(RegisterDto registerDto);
    
     Task<bool> CheckEmailExistAsync(string email);



    }
}
