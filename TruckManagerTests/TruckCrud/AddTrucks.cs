using FluentAssertions;
using System;
using System.Collections.Generic;
using TruckManager.Models;
using TruckManager.Services;
using Xunit;

namespace TruckManagerTests.TruckCrud
{
    public class AddTrucks
    {
        [Theory]
        [InlineData("New truck", 2021, 2021)]
        [InlineData("New truck", 2021, 2022)]
        public void NewTruckIsOk(string name, int manufacturingYear, int modelYear)
        {
            var service = Helpers.BuildService();

            var truck = new Truck
            {
                Name = name,
                Model = TruckModel.FrontalHigh,
                ManufacturingYear = manufacturingYear,
                ModelYear = modelYear
            };

            var result = service.Add(truck).Result;

            result.Entity.Should().BeEquivalentTo(truck);
            result.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void SameNameIsNotOk()
        {
            var service = Helpers.BuildService(new List<Guid> { Guid.NewGuid() });
            var truck = Helpers.BuildDefaultTruck(Guid.Empty);
            var result = service.Add(truck).Result;

            result.Entity.Should().BeNull();
            result.ValidationErrors.Should().Contain(new KeyValuePair<string, string>(nameof(truck.Name), ValidationMessages.TruckNameMustBeUnique));
        }

        [Fact]
        public void NotCurrentManufacturingYearIsNotOk()
        {
            var service = Helpers.BuildService();
            var truck = Helpers.BuildDefaultTruck(Guid.Empty);
            truck.ManufacturingYear = DateTime.Now.Year - 1;

            var result = service.Add(truck).Result;

            result.Entity.Should().BeNull();
            result.ValidationErrors.Should().Contain(new KeyValuePair<string, string>(nameof(truck.ManufacturingYear), ValidationMessages.ManufacturingYearMustBeCurrent));
        }

        [Theory]
        [InlineData(2021, 2020)]
        [InlineData(2021, 2023)]
        public void InvalidModelYearIsNotOk(int manufacturingYear, int modelYear)
        {
            var service = Helpers.BuildService();
            var truck = Helpers.BuildDefaultTruck(Guid.Empty);
            truck.ManufacturingYear = manufacturingYear;
            truck.ModelYear = modelYear;

            var result = service.Add(truck).Result;

            result.Entity.Should().BeNull();
            result.ValidationErrors.Should().Contain(new KeyValuePair<string, string>(nameof(truck.ModelYear), ValidationMessages.ModelYearMustBeSameOrSubsequentOfManufacturing));
        }
    }
}