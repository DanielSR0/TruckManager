using System;
using TruckManager.Infrastructure;
using TruckManager.Models;

namespace TruckManager.Services
{
    public interface ITruckService : IService<Truck>
    {
        public bool TruckExists(Guid id);

        public bool TruckExists(string name);
    }
}