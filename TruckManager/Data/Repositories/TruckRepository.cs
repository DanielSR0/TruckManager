using TruckManager.Infrastructure;
using TruckManager.Models;

namespace TruckManager.Data.Repositories
{
    public class TruckRepository : Repository<Truck>, ITruckRepository
    {
        public TruckRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}