using Garage2._0.Models.Entites;

namespace Garage2._0.Models.ViewModels;

public class VehicleDetailsViewModel
{

    public int Id { get; set; }
    public string RegisterNumber { get; set; }

    public VehicleType? VehicleType { get; set; }

    public string Color { get; set; }

    public string Brand { get; set; }

    public string Model { get; set; }

    public int NumberOfWheels { get; set; }

    public DateTime ArrivalTime { get; set; }

    public string CheckoutTime { get; set; }
}