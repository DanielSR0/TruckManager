using FluentAssertions;
using System;
using System.Collections.Generic;
using TruckManager.Models;
using TruckManager.Services;
using Xunit;

namespace TruckManagerTests.TruckCrud
{
    public class UpdateTrucks
    {
        [Fact]
        public void ChangeModelIsOk()
        {
            var defaultTruck = Guid.NewGuid();
            var service = Helpers.BuildService(new List<Guid> { defaultTruck });
            var truck = Helpers.BuildDefaultTruck(defaultTruck);
            truck.Model = TruckModel.FrontalMedium;

            var result = service.Update(truck).Result;

            result.Entity.Should().BeEquivalentTo(truck);
            result.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void ChangeNameIsNotOk()
        {
            var defaultTruck = Guid.NewGuid();
            var service = Helpers.BuildService(new List<Guid> { defaultTruck });
            var truck = Helpers.BuildDefaultTruck(defaultTruck);
            truck.Name = "Name has changed";

            var result = service.Update(truck).Result;

            result.Entity.Should().BeNull();
            result.ValidationErrors.Should().Contain(new KeyValuePair<string, string>(nameof(truck.Name), ValidationMessages.TruckNameIsReadonly));
        }

        [Fact]
        public void NotCurrentManufacturingYearIsNotOk()
        {
            var defaultTruck = Guid.NewGuid();
            var service = Helpers.BuildService(new List<Guid> { defaultTruck });
            var truck = Helpers.BuildDefaultTruck(defaultTruck);
            truck.ManufacturingYear = DateTime.Now.Year - 1;

            var result = service.Update(truck).Result;

            result.Entity.Should().BeNull();
            result.ValidationErrors.Should().Contain(new KeyValuePair<string, string>(nameof(truck.ManufacturingYear), ValidationMessages.ManufacturingYearMustBeCurrent));
        }

        [Theory]
        [InlineData(2021, 2020)]
        [InlineData(2021, 2023)]
        public void InvalidModelYearIsNotOk(int manufacturingYear, int modelYear)
        {
            var defaultTruck = Guid.NewGuid();
            var service = Helpers.BuildService(new List<Guid> { defaultTruck });
            var truck = Helpers.BuildDefaultTruck(defaultTruck);
            truck.ManufacturingYear = manufacturingYear;
            truck.ModelYear = modelYear;

            var result = service.Update(truck).Result;

            result.Entity.Should().BeNull();
            result.ValidationErrors.Should().Contain(new KeyValuePair<string, string>(nameof(truck.ModelYear), ValidationMessages.ModelYearMustBeSameOrSubsequentOfManufacturing));
        }
    }
}