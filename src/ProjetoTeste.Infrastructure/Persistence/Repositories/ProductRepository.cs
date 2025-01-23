using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entities;
using System.Linq.Expressions;

namespace ProjetoTeste.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public List<long> GetExistingProductInBrand(List<long> brandId)
        {
            return _dbSet.Where(i => brandId.Contains(i.BrandId)).Select(j => j.BrandId).ToList();
        }

        public async Task<Product?> GetWithIncludesAsync(long id, params Expression<Func<Product, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> GetByCode(string code)
        {
            return await _dbSet.Where(x => x.Code == code).FirstOrDefaultAsync();
        }
    }
}