using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Persistence.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext context) : base(context) { }

        public async Task<List<Brand>> GetListByCode(List<string> code)
        {
            return await _dbSet.Where(i => code.Contains(i.Code)).ToListAsync();
        }

        public string GetBrandNameById(long? id)
        {
            var brandName = _dbSet.FirstOrDefault(i => i.Id == id);
            return brandName.Name;
        }

        public async Task<List<Brand>> GetWithIncludesAll()
        {
            return await _dbSet.Include(i => i.ListProduct).ToListAsync();
        }
    }
}