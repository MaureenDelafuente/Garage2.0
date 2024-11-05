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
        public async Task<IActionResult> VehiclesList(string sortOrder)
        {
            ViewData["VehicleTypeSortParam"] = sortOrder == "VehicleType" ? "VehicleTypeDesc" : "VehicleType";
            ViewData["RegisterNumberSortParam"] = sortOrder == "RegisterNumber" ? "RegisterNumberDesc" : "RegisterNumber";
            ViewData["ArrivalTimeSortParam"] = sortOrder == "ArrivalTime" ? "ArrivalTimeDesc" : "ArrivalTime";
            ViewData["ColorSortParam"] = sortOrder == "Color" ? "ColorDesc" : "Color";
            ViewData["BrandSortParam"] = sortOrder == "Brand" ? "BrandDesc" : "Brand";

            var vehicles = await _context.Vehicle.ToListAsync();
            switch (sortOrder)
            {
                case "VehicleType":
                    vehicles = vehicles.OrderBy(v => Enum.GetName(typeof(VehicleType), v.VehicleType)).ToList();
                    break;
                case "VehicleTypeDesc":
                    vehicles = vehicles.OrderByDescending(v => Enum.GetName(typeof(VehicleType), v.VehicleType)).ToList();
                    break;
                case "RegisterNumber":
                    vehicles = vehicles.OrderBy(v => v.RegisterNumber).ToList();
                    break;
                case "RegisterNumberDesc":
                    vehicles = vehicles.OrderByDescending(v => v.RegisterNumber).ToList();
                    break;
                case "ArrivalTime":
                    vehicles = vehicles.OrderBy(v => v.ArrivalTime).ToList();
                    break;
                case "ArrivalTimeDesc":
                    vehicles = vehicles.OrderByDescending(v => v.ArrivalTime).ToList();
                    break;
                case "Color":
                    vehicles = vehicles.OrderBy(v => v.Color).ToList();
                    break;
                case "ColorDesc":
                    vehicles = vehicles.OrderByDescending(v => v.Color).ToList();
                    break;
                case "Brand":
                    vehicles = vehicles.OrderBy(v => v.Brand).ToList();
                    break;
                case "BrandDesc":
                    vehicles = vehicles.OrderByDescending(v => v.Brand).ToList();
                    break;
            }

            var colors = vehicles.Select(v => v.Color).Distinct().ToList();
            var brands = vehicles.Select(v => v.Brand).Distinct().ToList();
            ViewData["Colors"] = colors;
            ViewData["Brands"] = brands;

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
            vehicles = string.IsNullOrWhiteSpace(viewModel.Color)
                ? vehicles
                : vehicles.Where(m => m.Color.Contains(viewModel.Color));
            vehicles = string.IsNullOrWhiteSpace(viewModel.Brand)
                ? vehicles
                : vehicles.Where(m => m.Brand.Contains(viewModel.Brand));

            var colors =vehicles.Select(v => v.Color).Distinct().ToList();
            var brands =vehicles.Select(v => v.Brand).Distinct().ToList();
            ViewData["Colors"] = colors;
            ViewData["Brands"] = brands;

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckIn(VehicleCheckinViewModel viewModel)
        {
            if (_context.Vehicle.Any(v => v.RegisterNumber == viewModel.RegisterNumber))
            {
                ModelState.AddModelError("RegisterNumber", "This registration number already exists.");
            }

            if (ModelState.IsValid)
            {
                var vehicle = new Vehicle
                {
                    Id = viewModel.Id,
                    RegisterNumber = viewModel.RegisterNumber,
                    VehicleType = viewModel.VehicleType,
                    Color = viewModel.Color,
                    Brand = viewModel.Brand,
                    Model = viewModel.Model,
                    NumberOfWheels = viewModel.NumberOfWheels,
                    ArrivalTime = DateTime.Now // automatically set check-in time
                };

                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                // Feedback > after check-in go to details of that vehicle so user can see it is parked
                return RedirectToAction(nameof(Details), new {id = vehicle.Id});
            }

            viewModel.VehicleTypes = Enum.GetValues(typeof(VehicleType))
                .Cast<VehicleType>()
                .Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = v.ToString()
                }).ToList();

            // Return the view with validation errors if ModelState is invalid
            return View(viewModel);
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
