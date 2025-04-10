
namespace AuthAPI.Repositories
{
    public interface IRepository<T>
    {
        Task Add(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task Delete(T entity);
    }
}