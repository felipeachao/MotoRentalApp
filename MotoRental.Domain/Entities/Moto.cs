namespace MotoRental.Domain.Entities
{
    public class Moto(string model, string licensePlate, int year)
    {
        public Guid Id { get; set; }
        public required string Model { get; set; } = model;
        public required string LicensePlate { get; set; } = licensePlate;
        public int Year { get; set; } = year;
    }
}
