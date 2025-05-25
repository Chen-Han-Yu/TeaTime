using Microsoft.EntityFrameworkCore;
using TeaTime.Models;

namespace TeaTime.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    

    //新增程式碼
    public DbSet<Category> Categories { get; set; }
        //新增程式碼
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "茶飲",DisplayOrder = 1 },
                new Category { Id = 2, Name = "水果茶", DisplayOrder = 2 },
                new Category { Id = 3, Name = "奶茶", DisplayOrder = 3 }
                );
        }

    }
}