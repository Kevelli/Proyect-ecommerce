#nullable disable
using Microsoft.EntityFrameworkCore;
using Models;

namespace LeoEcommerce.Data;

public class DomainContext : DbContext
{
    public DomainContext (DbContextOptions<DomainContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Product { get; set; }
}