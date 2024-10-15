using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.G04.Api.Errors;
using Store.G04.Core.Entities.Identity;
using System.Security.Claims;

namespace Store.G04.Api.Extentions
{
    public static class UserManagerExtentions
    {
        public static async Task<AppUser> FindEmailWithAddressAsync(this UserManager<AppUser> usermanger,ClaimsPrincipal user)
        {
            var userEmail = user.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return null;
         var _user=await   usermanger.Users.Include(U=>U.Address).FirstOrDefaultAsync(U=>U.Email == userEmail);
            if (_user is null) return null;
            return _user;
        }


    }
}
