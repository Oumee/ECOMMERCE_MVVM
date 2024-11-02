using DOTNETECOMMERCE.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DOTNETECOMMERCE.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
       

        // Déclaration des DbSets pour Category et Product
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartProduct> Carts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DOTNETECOMMERCEDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
  
    }
}
