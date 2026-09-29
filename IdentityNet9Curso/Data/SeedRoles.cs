using Microsoft.AspNetCore.Identity;

namespace IdentityNet9Curso.Data
{
    public class SeedRoles
    {

        public static async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] strings = { "Admin", "User","Editor" };

            foreach (var role in strings)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }   

    }
}
