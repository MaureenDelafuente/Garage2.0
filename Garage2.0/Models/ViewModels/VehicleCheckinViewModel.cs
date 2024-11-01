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
        public string RegisterNumber { get; set; }

        public VehicleType? VehicleType { get; set; }

        public string Color { get; set; }


        [StringLength(15)]
        public string Brand { get; set; }


        [StringLength(16)]
        public string Model { get; set; }

        [Range(1,20)]
        public int NumberOfWheels { get; set; }

    }
}
