using Cards.Helpers;
using Core.Entities;
using Core.Exceptions;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cards.Extensions
{
    public static class DbMigrationServiceExtension
    {
        public static async Task<IApplicationBuilder> RunDbMigrations(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var services = scope.ServiceProvider;

            var _userManager = services.GetRequiredService<UserManager<SystemUser>>();

            var _roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            // Migrate on startup
            using (var serviceScope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CardDbContext>();

                if (context is null)
                    throw new NullDbContextException();

                context.Database.Migrate();
            }

            await SystemUserSeeder.SeedInitialUsers(_userManager, _roleManager);

            return app;
        }
    }
}
