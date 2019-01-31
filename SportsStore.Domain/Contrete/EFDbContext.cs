
using System.Data.Entity;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Contrete
{
    public class EFDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
