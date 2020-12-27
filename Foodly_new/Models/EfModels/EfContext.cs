using Foodly_new.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Foodly_new.Models.EfModels
{
    public class EFContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuPhotos> MenuPhotos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:foodly-server.database.windows.net,1433;Initial Catalog=Foodly;Persist Security Info=False;User ID=foodly-admin;Password=951753456Fo;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
