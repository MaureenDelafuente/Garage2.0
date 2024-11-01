using Garage2._0.Models.Entites;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage2._0.Models.ViewModels
{
    public class VehicleListViewModel

    {
        public IEnumerable<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public IEnumerable<SelectListItem> VehicleTypes { get; set; } = new List<SelectListItem>();

        public string? RegisterNumber { get; set; }
        public VehicleType? VehicleType { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
       
    }
}
