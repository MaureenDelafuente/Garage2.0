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
            var model = _context.Vehicle.Select(e => new VehicleListViewModel
            {
                Id = e.Id,
                VehicleType = e.VehicleType,
                RegisterNumber = e.RegisterNumber,
                ArrivalTime = e.ArrivalTime,
                Color = e.Color,
                Brand = e.Brand,
            });
            return View(await model.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
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
            return RedirectToAction(nameof(Receipt), new {id});
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

        // GET: Vehicles/Receipt/5
        public async Task<IActionResult> Receipt(int? id)
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
            var receiptViewModel = new ReceiptViewModel
            {
                RegisterNumber = vehicle.RegisterNumber,
                ArrivalTime = vehicle.ArrivalTime,
                CheckOutTime = vehicle.CheckoutTime,
                
            };
            return View(receiptViewModel);
        }

    }
}
