using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Garage2._0.Models.Entites;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage2._0.Models.ViewModels
{
    public class VehicleCheckinViewModel
    {
        public IEnumerable<SelectListItem> VehicleTypes { get; set; } = new List<SelectListItem>();

        public int Id { get; set; }

        [Required(ErrorMessage = "Registration number is required.")]
        [StringLength(10, ErrorMessage = "Registration number cannot be longer than 10 characters.")]
        [DisplayName("Register Number")]
        public string? RegisterNumber { get; set; }

        [Required(ErrorMessage = "Please select a vehicle type.")]
        [DisplayName("Vehicle Type")]
        public VehicleType? VehicleType { get; set; }

        [Required(ErrorMessage = "Color is required.")]
        [StringLength(15, ErrorMessage = "Color cannot exceed 15 characters.")]
        public string? Color { get; set; }

        [Required(ErrorMessage = "Brand is required.")]
        [StringLength(15, ErrorMessage = "Brand cannot exceed 15 characters.")]
        public string? Brand { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        [StringLength(16, ErrorMessage = "Model cannot exceed 16 characters.")]
        public string? Model { get; set; }

        [Required(ErrorMessage = "Please enter the number of wheels.")]
        [Range(1, 20, ErrorMessage = "Number of wheels must be between 1 and 20.")]
        [DisplayName("Total Wheels")]
        public int NumberOfWheels { get; set; }
    }
}
