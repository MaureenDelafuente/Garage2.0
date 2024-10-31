using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage2._0.Models.Entites;

namespace Garage2._0.Data
{
    public class Garage2_0Context : DbContext
    {
        public Garage2_0Context (DbContextOptions<Garage2_0Context> options)
            : base(options)
        {
        }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>().HasData(
               new Vehicle { Id = 1, RegisterNumber = "RGC 234", Brand = "BMW", Color = "Black", Model = "X6", NumberOfWheels = 4, VehicleType = VehicleType.Bicycle, ArrivalTime = DateTime.Parse("2024-10-30"), CheckoutTime = DateTime.Parse("2024-10-31") },
               new Vehicle { Id = 2, RegisterNumber = "INT 765", Brand = "VOLVO", Color = "white", Model = "V40", NumberOfWheels = 4, VehicleType = VehicleType.Boat, ArrivalTime = DateTime.Parse("2024-10-30"), CheckoutTime = DateTime.Parse("2024-10-31") },
               new Vehicle { Id = 3, RegisterNumber = "KMV 456", Brand = "AUDI", Color = "WHITE", Model = "A6", NumberOfWheels = 4, VehicleType = VehicleType.Scooter, ArrivalTime = DateTime.Parse("2024-10-30"), CheckoutTime = DateTime.Parse("2024-10-31") },
               new Vehicle { Id = 4, RegisterNumber = "ZDG 980", Brand = "VOLVO", Color = "Black", Model = "GX", NumberOfWheels = 4, VehicleType = VehicleType.Bicycle, ArrivalTime = DateTime.Parse("2024-10-30"), CheckoutTime = DateTime.Parse("2024-10-31") },
               new Vehicle { Id = 5, RegisterNumber = "LKW 285", Brand = "SAAB", Color = "Black", Model = "A5", NumberOfWheels = 4, VehicleType = VehicleType.Van, ArrivalTime = DateTime.Parse("2024-10-30"), CheckoutTime = DateTime.Parse("2024-10-31") },
               new Vehicle { Id = 6, RegisterNumber = "FFC 170", Brand = "KIA", Color = "RED", Model = "BX", NumberOfWheels = 4, VehicleType = VehicleType.Bicycle, ArrivalTime = DateTime.Parse("2024-10-30"), CheckoutTime = DateTime.Parse("2024-10-31") },
               new Vehicle { Id = 7, RegisterNumber = "XZS 376", Brand = "BMW", Color = "GREEN", Model = "X5", NumberOfWheels = 4, VehicleType = VehicleType.Car, ArrivalTime = DateTime.Parse("2024-10-30"), CheckoutTime = DateTime.Parse("2024-10-31") }
               );
        }
        */
        public DbSet<Garage2._0.Models.Entites.Vehicle> Vehicle { get; set; } = default!;
    }
}
