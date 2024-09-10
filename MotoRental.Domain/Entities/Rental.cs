using MotoRental.Domain.Enums;

namespace MotoRental.Domain.Entities
{
    public class Rental
    {
        public Guid Id { get; set; }
        public Guid DeliveryPersonId { get; set; }
        public Guid MotoId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public RentalPlan Plan { get; set; }
        public decimal TotalCost { get; set; }
        public decimal PenaltyCost { get; set; }
    }
}
