namespace MotoRental.Infrastructure.Repositories
{
    public interface IDeliveryPersonRepository
    {
        Task<DeliveryPerson> GetByIdAsync(Guid id);
        Task<IEnumerable<DeliveryPerson>> GetAllAsync();
        Task AddAsync(DeliveryPerson deliveryPerson);
        Task UpdateAsync(DeliveryPerson deliveryPerson);
        Task DeleteAsync(Guid id);
        
        Task<bool> ExistsByCnpjAsync(string cnpj);
        Task<bool> ExistsByCnhNumberAsync(string cnhNumber);
    }
}
