using PointAppWithCleanArchitecture.Domain.Models;

namespace PointAppWithCleanArchitecture.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task UpdatePoints();
    }
}
