namespace Garage2._0.Models.Entites
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string RegisterNumber { get; set; }

        public VehicleType? VehicleType {  get; set; }

        public string Color { get; set; }

        public string Brand {  get; set; }

        public string Model {  get; set; }

        public int NumberOfWheels { get; set; }

        public DateTime ArrivalTime { get; set; }

        public DateTime CheckoutTime { get; set; }

    }
}
