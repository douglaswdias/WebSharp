using Microsoft.EntityFrameworkCore;
using WebSharp.Api.Models;

namespace WebSharp.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    DbSet<Product> Products { get; set; }
    DbSet<Category> Categories { get; set; }
}