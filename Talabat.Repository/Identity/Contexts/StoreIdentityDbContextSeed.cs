using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Identity;

namespace Talabat.Repository.Identity.Contexts
{
    public static class StoreIdentityDbContextSeed
    {
        public async static Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Count() == 0)
            {

                var user = new AppUser()
                {
                    Email = "ali@gmail.com",
                    DisplayName = "ali mohamed",
                    UserName = "ali.mohamed",
                    PhoneNumber = "012222222",
                    Address = new Address()
                    {
                        FName = "ali",
                        LName = "mohamed",
                        City = "menofia",
                        Street = "salah eldien",
                        Country="egypt"
                    }
                };

                await userManager.CreateAsync(user, "P@ssW0rd");
            }


        }
    }
}
