namespace Garage2._0.Models.ViewModels;

public class VehicleDetailsViewModel
{
    public string RegisterNumber { get; set; }

    public string VehicleType { get; set; }

    public string Color { get; set; }

    public string Brand { get; set; }

    public string Model { get; set; }

    public int NumberOfWheels { get; set; }

    public DateTime ArrivalTime { get; set; }

    public DateTime CheckoutTime { get; set; }
}