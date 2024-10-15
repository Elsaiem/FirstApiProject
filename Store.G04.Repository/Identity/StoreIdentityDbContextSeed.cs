using Microsoft.AspNetCore.Identity;
using Store.G04.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Repository.Identity
{
    public class StoreIdentityDbContextSeed
    {

        public static async Task SeedAppUserAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {

                var user = new AppUser()
                {
                    Email = "y.saiem2111@gmaail.com",
                    DisplayName = "Yousef Elsaiem",
                    UserName = "Elsaiem",
                    PhoneNumber = "01023164133",
                    Address = new Address()
                    {
                        FName = "Yousef",
                        LName = "ELsaiem",
                        City = "Fayoum",
                        Country = "Egypt",
                        Streat = "Eljazier"
                    }

                };
                await _userManager.CreateAsync(user, "P@ssW0rd");

            }
        }


    }
}
