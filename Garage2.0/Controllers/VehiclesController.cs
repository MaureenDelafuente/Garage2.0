using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2._0.Data;
using Garage2._0.Models.Entites;
using Garage2._0.Models.ViewModels;

namespace Garage2._0.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly Garage2_0Context _context;

        public VehiclesController(Garage2_0Context context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> VehiclesList()
        {
            var vehicles = await _context.Vehicle.ToListAsync();

            var model = new VehicleListViewModel
            {
                Vehicles = vehicles,
                VehicleTypes = vehicles.Select(m => m.VehicleType)
                .Distinct()
                .Select(g => new SelectListItem
                {
                    Text = g.ToString(),
                    Value = g.ToString()
                })
                .ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> Filter(VehicleListViewModel viewModel)
        {
            var vehicles = string.IsNullOrWhiteSpace(viewModel.RegisterNumber) ?
                _context.Vehicle :
                _context.Vehicle.Where(m => m.RegisterNumber.Contains(viewModel.RegisterNumber));

            vehicles = viewModel.VehicleType is null ?
                vehicles :
                vehicles.Where(m => m.VehicleType == viewModel.VehicleType);

            var model = new VehicleListViewModel
            {
                Vehicles = await vehicles.ToListAsync(),
                VehicleTypes = Enum.GetValues(typeof(VehicleType))
                    .Cast<VehicleType>()
                    .Select(v => new SelectListItem
                    {
                        Text = v.ToString(),
                        Value = v.ToString()
                    })
                    .ToList()
            };

            return View(nameof(VehiclesList), model);
        }


        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.Where(e => e.Id == id).Select(v => new VehicleDetailsViewModel
            {
                Id = v.Id,
                RegisterNumber = v.RegisterNumber,
                VehicleType = v.VehicleType,
                Brand = v.Brand,
                Model = v.Model,
                Color = v.Color,
                NumberOfWheels = v.NumberOfWheels,
                ArrivalTime = v.ArrivalTime,
                CheckoutTime = v.CheckoutTime == DateTime.MinValue ? "Vehicle is still in parking" : v.CheckoutTime.ToString("yyyy-MM-dd HH:mm:ss")
            })
            .FirstOrDefaultAsync();

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/CheckIn
        public IActionResult CheckIn()
        {
            var viewModel = new VehicleCheckinViewModel
            {
                VehicleTypes = Enum.GetValues(typeof(VehicleType))
                    .Cast<VehicleType>()
                    .Select(v => new SelectListItem
                    {
                        Text = v.ToString(),
                        Value = v.ToString()
                    })
                    .ToList()
            };

            return View(viewModel);
        }

        // POST: Vehicles/CheckIn
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckIn([Bind("Id,RegisterNumber,VehicleType,Color,Brand,Model,NumberOfWheels")] Vehicle vehicle)
        {
            vehicle.ArrivalTime = DateTime.Now;// Added so we automatically get the checkin time
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(VehiclesList));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            var vehicleEditViewModel = new VehicleEditViewModel
            {
                Id = vehicle.Id,
                RegisterNumber = vehicle.RegisterNumber,
                Color = vehicle.Color,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                NumberOfWheels = vehicle.NumberOfWheels,
                VehicleType = vehicle.VehicleType,
                VehicleTypes = Enum.GetValues(typeof(VehicleType))
           .Cast<VehicleType>()
           .Select(v => new SelectListItem
           {
               Value = ((int)v).ToString(),
               Text = v.ToString()
           }).ToList()
            };


            return View(vehicleEditViewModel);
        }

        // POST: Vehicles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleEditViewModel vehicleViewModel)
        {
            if (id != vehicleViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Fetch the original vehicle from the database
                var vehicle = await _context.Vehicle.FindAsync(id);
                if (vehicle == null)
                {
                    return NotFound();
                }

                // Update the vehicle properties
                vehicle.RegisterNumber = vehicleViewModel.RegisterNumber;
                vehicle.Color = vehicleViewModel.Color;
                vehicle.Brand = vehicleViewModel.Brand;
                vehicle.Model = vehicleViewModel.Model;
                vehicle.NumberOfWheels = vehicleViewModel.NumberOfWheels;
                vehicle.VehicleType = vehicleViewModel.VehicleType;

                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(VehiclesList));
            }

            // Repopulate the VehicleTypes in case of validation errors
            vehicleViewModel.VehicleTypes = Enum.GetValues(typeof(VehicleType))
                .Cast<VehicleType>()
                .Select(v => new SelectListItem
                {
                    Value = ((int)v).ToString(),
                    Text = v.ToString()
                }).ToList();

            return View(vehicleViewModel);
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicle.Any(e => e.Id == id);
        }


        // GET: Vehicles/CheckOut/5
        public async Task<IActionResult> CheckOut(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/CheckOut/5
        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOutConfirmed(int id)
        {
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle != null)
            {
                //_context.Vehicle.Remove(vehicle); changed to next line
                vehicle.CheckoutTime = DateTime.Now;// we dont remove it so we can see the vechile and its checkout time

            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(VehiclesList));
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Confirm")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(VehiclesList));
        }


    }
}
