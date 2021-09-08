using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckManager.Data.Repositories;
using TruckManager.Infrastructure;
using TruckManager.Models;

namespace TruckManager.Services
{
    public class TruckService : ITruckService
    {
        private readonly ITruckRepository _repository;

        public TruckService(ITruckRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Truck>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<Truck> Get(Guid id)
        {
            return _repository.Get(id);
        }

        public async Task<ServiceResult<Truck>> Add(Truck entity)
        {
            var result = new ServiceResult<Truck>
            {
                ValidationErrors = await Validate(entity)
            };

            if (!result.ValidationErrors.Any())
            {
                result.Entity = await _repository.Add(entity);
            }

            return result;
        }

        public async Task<ServiceResult<Truck>> Update(Truck entity)
        {
            var result = new ServiceResult<Truck>
            {
                ValidationErrors = await Validate(entity)
            };

            if (!result.ValidationErrors.Any())
            {
                result.Entity = await _repository.Update(entity);
            }

            return result;
        }

        public async Task<Truck> Delete(Guid id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
            {
                return entity;
            }

            return await _repository.Delete(entity);
        }

        public bool TruckExists(Guid id)
        {
            return _repository.GetQueryable().Any(e => e.Id == id);
        }

        public bool TruckExists(string name)
        {
            return _repository.GetQueryable().Any(e => e.Name == name);
        }

        private async Task<Dictionary<string, string>> Validate(Truck truck)
        {
            var errors = new Dictionary<string, string>();
            var originalName = "";
            var originalManufacturingYear = 0;

            if (truck.Id != Guid.Empty)
            {
                var original = await _repository.Get(truck.Id);

                if (original != null)
                {
                    originalName = original.Name;
                    originalManufacturingYear = original.ManufacturingYear;
                }
            }
            else if (TruckExists(truck.Name))
            {
                errors.Add(nameof(truck.Name), ValidationMessages.TruckNameMustBeUnique);
            }

            if (!string.IsNullOrWhiteSpace(originalName) && truck.Name != originalName)
            {
                errors.Add(nameof(truck.Name), ValidationMessages.TruckNameIsReadonly);
            }

            if (truck.ManufacturingYear != originalManufacturingYear && truck.ManufacturingYear != DateTime.Now.Year)
            {
                errors.Add(nameof(truck.ManufacturingYear), ValidationMessages.ManufacturingYearMustBeCurrent);
            }

            if (truck.ModelYear != truck.ManufacturingYear && truck.ModelYear != truck.ManufacturingYear + 1)
            {
                errors.Add(nameof(truck.ModelYear), ValidationMessages.ModelYearMustBeSameOrSubsequentOfManufacturing);
            }

            return errors;
        }
    }
}