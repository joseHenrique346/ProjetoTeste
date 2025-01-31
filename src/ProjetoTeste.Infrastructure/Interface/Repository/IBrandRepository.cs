using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Interface.Repository
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<List<Brand>> GetListByCode(List<string> code);
        Task<List<Brand>> GetWithIncludesAll();
        string GetBrandNameById(long? id);
    }
}