using CryptoBot.Domain.Models;

namespace CryptoBot.Domain.Interfaces.Repositories;

public interface IOrderRepository
{
    IEnumerable<Order> GetAll();
    void Save(Order order);
}