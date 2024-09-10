public class DeliveryPerson
{
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public required string Name { get; set; }
    public required string Cnpj { get; set; }
    public DateTime DateOfBirth { get; set; }
    public required string CnhNumber { get; set; }
    public CnhType CnhType { get; set; }
    public required string CnhImagePath { get; set; } = string.Empty; 
}

public enum CnhType
{
    A,
    B,
    A_B
}
