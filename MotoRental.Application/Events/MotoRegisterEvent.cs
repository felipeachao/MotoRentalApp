namespace MotoRental.Application.Events
{
    public class MotoRegisterEvent(string model, string licensePlate, int Year)
    {
        public Guid MotoId { get; set; }
        public required int Year { get; set; } = Year;
        public required string Model { get; set; } = model;
        public required string LicensePlate { get; set; } = licensePlate;
    }
}
