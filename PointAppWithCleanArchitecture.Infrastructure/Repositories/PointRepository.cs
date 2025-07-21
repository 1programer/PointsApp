using PointAppWithCleanArchitecture.Data;
using PointAppWithCleanArchitecture.Interfaces;

namespace PointAppWithCleanArchitecture.Repositories
{
    public class PointRepository(AppDbContext context) : Repository<Domain.Models.Point>(context), IPointRepository
    {
    }
}
