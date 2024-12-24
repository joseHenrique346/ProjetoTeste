using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entities;


namespace ProjetoTeste.Infrastructure.Persistence.Repositories
{
    public class ClientRepository : Repository<Client>
    {
        public ClientRepository(AppDbContext context) : base(context) { }
    }
}
