using Garage2._0.Models.Entites;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage2._0.Models.ViewModels
{
    public class VehicleEditViewModel
    {
        public IEnumerable<SelectListItem> VehicleTypes { get; set; } = new List<SelectListItem>();

        public int Id { get; set; }
        public VehicleType? VehicleType { get; set; }
        public string RegisterNumber { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int NumberOfWheels { get; set; }

       
    }
}
