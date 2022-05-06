using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Persistence
{
    public class IdentityUserSeed
    {
        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<UserManager<IdentityUser>>())
                {
                    await EnsureSeedData(context);
                }
            }
        }

        private static async Task EnsureSeedData(UserManager<IdentityUser> userManager)
        {
            Console.WriteLine("Seeding database...");

            if (!userManager.Users.Any())
            {
                Console.WriteLine("Users being populated");
                var user = new IdentityUser
                {
                    Email = "salman@askhorizons.com",
                    PhoneNumber = "+923308451234",
                    UserName = "salman1277"
                };

                await userManager.CreateAsync(user, "p@$$w0rD");
            }
            else
            {
                Console.WriteLine("Users already populated");
            }


            Console.WriteLine("Done seeding database.");
            Console.WriteLine();
        }
    }
}
