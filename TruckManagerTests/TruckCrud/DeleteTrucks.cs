using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace TruckManagerTests.TruckCrud
{
    public class DeleteTrucks
    {
        [Fact]
        public void DeleteExistingTruckIsOk()
        {
            var defaultTruck = Guid.NewGuid();
            var service = Helpers.BuildService(new List<Guid> { defaultTruck });
            var truck = Helpers.BuildDefaultTruck(defaultTruck);

            var result = service.Delete(defaultTruck).Result;

            result.Should().BeEquivalentTo(truck);
        }

        [Fact]
        public void DeleteNonExistingTruckIsNotOk()
        {
            var defaultTruck = Guid.NewGuid();
            var service = Helpers.BuildService(new List<Guid> { defaultTruck });

            var result = service.Delete(Guid.NewGuid()).Result;

            result.Should().BeNull();
        }
    }
}