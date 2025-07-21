using PointAppWithCleanArchitecture.Application.DTOS;
using PointAppWithCleanArchitecture.Domain.Models;

namespace PointAppWithCleanArchitecture.Interfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<PointDto> BuyItem(Guid Id, string userId, decimal quantity);
    }
}
