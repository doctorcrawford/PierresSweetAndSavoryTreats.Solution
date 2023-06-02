using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Shop.Models
{
  public class ShopContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Flavor> Flavors { get; set; }
    public DbSet<Treat> Treats { get; set; }
    public DbSet<FlavorTreat> FlavorTreats { get; set; }

    public ShopContext(DbContextOptions options) : base(options) { }
  }
}