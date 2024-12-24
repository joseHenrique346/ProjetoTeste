using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Persistence.Repositories
{
    public class BrandRepository : Repository<Brand>
    {
        public BrandRepository(AppDbContext context) : base(context) { }
    }
}
