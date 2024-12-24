using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entities;


namespace ProjetoTeste.Infrastructure.Persistence.Repositories
{
    public class ClientRepository(AppDbContext context) : Repository<Client>(context)
    {
    }
}
