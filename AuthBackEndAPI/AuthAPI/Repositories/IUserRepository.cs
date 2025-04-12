using AuthAPI.Models;

namespace AuthAPI.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task Add(User user);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(Guid id);
        Task<User> GetByEmail(string email);
        Task Delete(User user);
        Task Change();
    }

}