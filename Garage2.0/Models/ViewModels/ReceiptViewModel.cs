namespace Garage2._0.Models.ViewModels;

public class ReceiptViewModel
{
    public const double Priceperminute = 0.5;

    public string RegisterNumber { get; set; }

    public DateTime ArrivalTime { get; set; }

    public DateTime CheckOutTime { get; set; }

    public TimeSpan ParkingPeriod
    {
        get => CheckOutTime - ArrivalTime;
    }

    public int Price
    {
        get => (int) Math.Round(ParkingPeriod.TotalMinutes * Priceperminute);
    }
}