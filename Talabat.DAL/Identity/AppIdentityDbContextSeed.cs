using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Muhammed Abdelreda",
                    UserName = "Muhammed",
                    Email = "mabdelreda0@gmail.com",
                    PhoneNumber = "01123183757",
                    Address = new Address()
                    {
                        FirstName = "Muhammed",
                        LastName = "Abdelreda",
                        Country = "Egypt",
                        City = "Giza",
                        Street = "HaramStreet",
                        //AppUserId = "123",
                    }
                };
                await userManager.CreateAsync(user,"Pa$$w0rd"); //P@ssw0rd
            }
        }
    }
}
