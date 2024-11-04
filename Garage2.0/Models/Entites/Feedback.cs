namespace Garage2._0.Models.Entites
{
    public class Feedback
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Rating { get; set; } = string.Empty;
        public string FeedbackMessage { get; set; } = string.Empty;
    }
}
