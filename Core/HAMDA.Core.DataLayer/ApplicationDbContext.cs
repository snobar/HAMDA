using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HAMDA.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        #region Costumer
        public DbSet<HAMDA.Models.EntityModels.Costumer.CostumerRegister> Costumers { get; set; }
        #endregion
        
        #region System
        public DbSet<HAMDA.Models.EntityModels.System.Attachment> Attachments { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Roles
            var AdminRoles = new IdentityRole("Admin")
            {
                Id = "3ea9a713-aaf4-419d-bb97-849714fe91a2",
                NormalizedName = "Admin"
            };

            modelBuilder.Entity<IdentityRole>().HasData(AdminRoles);
            #endregion

            #region User
            var userId = "20dcdd1d-134c-4ce7-ba60-63aaa7535590";

            var user = new IdentityUser { Id= userId, UserName = "Admin@hamda.com", Email = "Admin@hamda.com", NormalizedUserName = "Admin@hamda.com",PasswordHash= "AQAAAAIAAYagAAAAEPkTIuEco1Bk2uZSa78Gp+82zNJA5MC3+zgF/Y6qHnuRrqFaVbFU/ksBSKVCCa2d8w==", EmailConfirmed = true };
            modelBuilder.Entity<IdentityUser>().HasData(user);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = userId,
                RoleId = "3ea9a713-aaf4-419d-bb97-849714fe91a2"
            });
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
