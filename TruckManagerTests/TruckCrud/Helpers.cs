using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckManager.Data.Repositories;
using TruckManager.Models;
using TruckManager.Services;

namespace TruckManagerTests.TruckCrud
{
    internal static class Helpers
    {
        internal static TruckService BuildService(List<Guid> defaultDataIds = null)
        {
            defaultDataIds ??= new List<Guid>();
            var defaultData = new List<Truck>();

            foreach (var id in defaultDataIds)
            {
                defaultData.Add(BuildDefaultTruck(id));
            }

            var repositoryMock = new Mock<ITruckRepository>();

            repositoryMock
                .Setup(r => r.GetQueryable())
                .Returns(defaultData.AsQueryable());

            repositoryMock
                .Setup(r => r.Get(It.IsAny<Guid>()))
                .Returns<Guid>(guid => Task.FromResult(defaultData.Find(t => t.Id == guid)));

            repositoryMock
                .Setup(r => r.Add(It.IsAny<Truck>()))
                .Returns<Truck>(t => Task.FromResult(t));

            repositoryMock
                .Setup(r => r.Update(It.IsAny<Truck>()))
                .Returns<Truck>(t => Task.FromResult(t));

            repositoryMock
                .Setup(r => r.Delete(It.IsAny<Truck>()))
                .Returns<Truck>(t => Task.FromResult(t));

            var service = new TruckService(repositoryMock.Object);

            return service;
        }

        internal static Truck BuildDefaultTruck(Guid id)
        {
            return new Truck
            {
                Id = id,
                Name = "Existing truck",
                Model = TruckModel.FrontalHigh,
                ManufacturingYear = 2021,
                ModelYear = 2021
            };
        }
    }
}