using PointAppWithCleanArchitecture.Data;
using PointAppWithCleanArchitecture.Interfaces;
using PointAppWithCleanArchitecture.Domain.Models;

namespace PointAppWithCleanArchitecture.Repositories
{
    public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository
    {
        public async Task UpdatePoints()
        {
            IEnumerable<User> users = GetAll();
            if (users == null)
                throw new Exception("Users not found");
            foreach (User user in users)
            {
                decimal TotalPoints = 0;
                foreach (Point point in context.Point)
                    if (point.IsRedeemed)
                        continue;
                    else if (point.UserId == Guid.Parse(user.Id))
                    {
                        TotalPoints += point.Amount;
                        point.IsRedeemed = true;

                    }

                user.Points += TotalPoints;
            }
            
            await context.SaveChangesAsync();
        }
    }
}
