using PointAppWithCleanArchitecture.Data;
using PointAppWithCleanArchitecture.Interfaces;
using PointAppWithCleanArchitecture.Domain.Models;
using PointAppWithCleanArchitecture.Application.DTOS;

namespace PointAppWithCleanArchitecture.Repositories
{
    public class ItemRepository(AppDbContext context) : Repository<Item>(context), IItemRepository
    {
        public async Task<PointDto> BuyItem(Guid Id, string userId, decimal itemQuantity)
        {
            Item item = GetById(Id);
            User user = await context.Users.FindAsync(userId);
            if (user == null) throw new Exception("User Not Found");
            else if (item == null) throw new Exception("Item Not Found");
            else if (itemQuantity <= 0) throw new Exception($"You cant buy{itemQuantity} items");
            else if (user.Points < itemQuantity * item.Price) throw new Exception($"You need {item.Price*itemQuantity-user.Points} more points");
            else
            {
                PointDto point = new PointDto
                {
                    amount = -itemQuantity * item.Price,
                    UserId = user.Id,
                    IsRedeemed = false
                };
                await context.SaveChangesAsync();
                return point;
            }
        }
    }
}
