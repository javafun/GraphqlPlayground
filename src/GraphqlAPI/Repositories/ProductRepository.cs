using GraphqlDemo.Data;
using GraphqlDemo.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphqlDemo.Repositories
{
    public class ProductRepository
    {
        private readonly GraphqlDbContext _dbContext;

        public ProductRepository(GraphqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Product>> GetAll()
        {
            return _dbContext.Products.ToListAsync();
        }
    }
}
