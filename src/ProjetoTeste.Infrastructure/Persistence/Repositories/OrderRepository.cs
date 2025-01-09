using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public async Task<List<Order?>> GetWithIncludesAsync(long id)
        {
            var teste = await _context.Set<Order>().Include(x => x.ListProductOrder).Where(x => x.Id == id).ToListAsync();
            return teste;
        }

        public async Task<List<Order?>> GetWithIncludesAsync()
        {
            var teste = await _context.Set<Order>().Include(x => x.ListProductOrder).ToListAsync();
            return teste;
        }
    }
}