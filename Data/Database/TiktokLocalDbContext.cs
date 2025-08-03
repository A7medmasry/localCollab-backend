using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TiktokLocalAPI.Core.Models.Order;
using TiktokLocalAPI.Core.Models.Service;
using TiktokLocalAPI.Data.Models.Chat;
using TiktokLocalAPI.Data.Models.User;

namespace TiktokLocalAPI.Data.Database
{
    /// <summary>
    /// TiktokLocalDbContext is a custom DbContext for managing identity-related data.
    /// </summary>
    public class TiktokLocalDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the DbSet for managing user data.
        /// </summary>
        public DbSet<UserModel> Users { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for managing password reset request data.
        /// </summary>
        public DbSet<PasswordResetRequestModel> PasswordResetRequests { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for managing user session data.
        /// </summary>
        public DbSet<SessionModel> Sessions { get; set; }

        public DbSet<ServiceModel> Services { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<ReviewModel> Reviews { get; set; }

        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TiktokLocalDbContext"/> class.
        /// </summary>
        /// <param name="options">The DbContext options.</param>
        public TiktokLocalDbContext(DbContextOptions<TiktokLocalDbContext> options)
            : base(options) { }

        /// <summary>
        /// Configures the application middleware during startup.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment.</param>
        /// <param name="roleManager">The role manager for managing identity roles.</param>
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            RoleManager<IdentityRole> roleManager
        ) { }

        /// <summary>
        /// Configures the database model and relationships.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>().Property(u => u.Role).HasConversion<int>();

            modelBuilder.Entity<UserModel>().OwnsOne(u => u.BusinessInformation);
            modelBuilder
                .Entity<UserModel>()
                .OwnsOne(
                    u => u.Creator,
                    creator =>
                    {
                        creator.OwnsMany(c => c.Platforms);
                    }
                );
            modelBuilder.Entity<UserModel>().OwnsOne(u => u.StatusUser);
            modelBuilder
                .Entity<UserModel>()
                .OwnsMany(
                    u => u.ServiceProvider,
                    sp =>
                    {
                        sp.OwnsMany(p => p.Category);
                    }
                );
            modelBuilder.Entity<ServiceModel>().HasIndex(s => s.Slug).IsUnique();
            modelBuilder
                .Entity<OrderModel>()
                .HasOne(o => o.FromUser)
                .WithMany(u => u.OrdersSent)
                .HasForeignKey(o => o.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<OrderModel>()
                .HasOne(o => o.ToUser)
                .WithMany(u => u.OrdersReceived)
                .HasForeignKey(o => o.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<ReviewModel>()
                .HasOne(r => r.FromUser)
                .WithMany(u => u.ReviewsGiven)
                .HasForeignKey(r => r.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<ReviewModel>()
                .HasOne(r => r.ToUser)
                .WithMany(u => u.ReviewsReceived)
                .HasForeignKey(r => r.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Example of setting default character set and collation
            modelBuilder.UseCollation("utf8mb4_general_ci");
        }
    }
}
