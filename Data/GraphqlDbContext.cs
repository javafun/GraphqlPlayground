using GraphqlDemo.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphqlDemo.Data
{
    public class GraphqlDbContext : DbContext
    {
        public GraphqlDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
    }
}
