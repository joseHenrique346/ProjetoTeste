using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Interface.Repository
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<Brand> GetByCode(string code);
        string GetBrandNameById(long? id);
    }
}