namespace Garage2._0.Models.Entites
{
    public class Vehicle
    {
        public int Id { get; set; }
        string RegisterNumber { get; set; }

        string VehicleType {  get; set; }

        string Color { get; set; }

        string Brand {  get; set; }

        string Model {  get; set; }

        int NumberOfWheels { get; set; }

        DateTime ArrivalTime { get; set; }

        DateTime CheckoutTime { get; set; }

    }
}
