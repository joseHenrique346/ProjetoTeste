namespace ProjetoTeste.Infrastructure.Interface.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<List<T?>> GetListByListId(List<long> id);
        Task<List<T>> CreateAsync(List<T> entities);
        Task<List<T>> Update(List<T> entities);
        Task<bool> DeleteAsync(List<T> entities);
    }
}