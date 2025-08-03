using System.Diagnostics;
using Framework.Enums;
using Microsoft.EntityFrameworkCore;
using TiktokLocalAPI.Data.Models.User;

namespace TiktokLocalAPI.Data.Database
{
    /// <summary>
    /// Provides methods to prepare and seed the database during application startup.
    /// </summary>
    public class PrepareDb
    {
        /// <summary>
        /// Performs initial population and migration of the database.
        /// Ensures that required entities (e.g., an admin account) exist.
        /// </summary>
        /// <param name="app">The application builder instance used to access service scopes.</param>
        public static void PreparePopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<TiktokLocalDbContext>();

                if (dbContext == null)
                {
                    return;
                }

                SeedData(dbContext);
            }
        }

        /// <summary>
        /// Applies pending migrations and seeds an admin user if one does not already exist.
        /// </summary>
        /// <param name="context">The database context used to access and update the database.</param>
        private static void SeedData(TiktokLocalDbContext context)
        {
            try
            {
                context.Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Could not run migration: {e.Message}");
            }

            // Add admin account if missing
            if (!context.Users.Any(u => u.Email == "admin@admin.com"))
            {
                Debug.WriteLine("--> Seeding admin");

                var adminUser = new UserModel
                {
                    Email = "admin@admin.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Q1!werty"),
                    Role = SystemRole.Admin,
                    FirstName = "Admin",
                    LastName = "User",
                };

                context.Users.Add(adminUser);
                context.SaveChanges();
            }
        }
    }
}
