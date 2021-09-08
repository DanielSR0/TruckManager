using System;

namespace TruckManager.Infrastructure
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}