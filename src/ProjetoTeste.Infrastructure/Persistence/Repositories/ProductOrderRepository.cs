using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Persistence.Repositories
{
    public class ProductOrderRepository : Repository<ProductOrder>, IProductOrderRepository
    {
        public ProductOrderRepository(AppDbContext context) : base(context) { }

        public async Task<ProductOrder?> GetAsync(long orderId, long productId)
        {
            return await _dbSet.FirstOrDefaultAsync(po => po.OrderId == orderId && po.ProductId == productId);
        }
    }
}
