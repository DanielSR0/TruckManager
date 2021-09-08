using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TruckManager.Infrastructure;
using TruckManager.Models;
using TruckManager.Services;

namespace TruckManager.Controllers
{
    [Authorize]
    public class TrucksController : Controller
    {
        private readonly ITruckService _service;

        public TrucksController(ITruckService service)
        {
            _service = service;
        }

        // GET: Trucks
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        // GET: Trucks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _service.Get(id.Value);

            if (truck == null)
            {
                return NotFound();
            }

            return View(truck);
        }

        // GET: Trucks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trucks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Model,ManufacturingYear,ModelYear")] Truck truck)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Add(truck);

                if (!result.ValidationErrors.Any())
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.ValidationErrors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }

            return View(truck);
        }

        // GET: Trucks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _service.Get(id.Value);

            if (truck == null)
            {
                return NotFound();
            }

            return View(truck);
        }

        // POST: Trucks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Model,ManufacturingYear,ModelYear")] Truck truck)
        {
            if (id != truck.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ServiceResult<Truck> result;

                try
                {
                    result = await _service.Update(truck);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_service.TruckExists(truck.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (!result.ValidationErrors.Any())
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.ValidationErrors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }

            return View(truck);
        }

        // GET: Trucks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _service.Get(id.Value);

            if (truck == null)
            {
                return NotFound();
            }

            return View(truck);
        }

        // POST: Trucks/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var truck = await _service.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
