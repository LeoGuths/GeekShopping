using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Model.Context;

public class MySqlContext : DbContext
{
    public MySqlContext() { }
    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 2,
            Name = "T-shirt",
            Price = new decimal(69.9),
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris ut egestas ipsum, non venenatis nulla. Morbi iaculis aliquam urna. Curabitur at volutpat arcu. Morbi at dictum massa, in sodales velit. Nulla facilisi. Curabitur quam turpis. ",
            CategoryName = "T-shirt",
            ImageUrl = "https://gitlab.com/uploads/-/system/user/avatar/12037378/avatar.png?width=400"
        });
        
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 3,
            Name = "Bleach Aizen Action Figure PVC",
            Price = new decimal(109.9),
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris ut egestas ipsum, non venenatis nulla. Morbi iaculis aliquam urna. Curabitur at volutpat arcu. Morbi at dictum massa, in sodales velit. Nulla facilisi. Curabitur quam turpis. ",
            CategoryName = "Action Figure",
            ImageUrl = "https://gitlab.com/uploads/-/system/user/avatar/12037378/avatar.png?width=400"
        });
        
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 4,
            Name = "Sweatshirt",
            Price = new decimal(329.9),
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris ut egestas ipsum, non venenatis nulla. Morbi iaculis aliquam urna. Curabitur at volutpat arcu. Morbi at dictum massa, in sodales velit. Nulla facilisi. Curabitur quam turpis. ",
            CategoryName = "Sweatshirt",
            ImageUrl = "https://gitlab.com/uploads/-/system/user/avatar/12037378/avatar.png?width=400"
        });
    }
}