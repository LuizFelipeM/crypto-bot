using CryptoBot.Domain.Interfaces.Repositories;
using CryptoBot.Domain.Models;
using SQLite;

namespace CryptoBot.Infrastructure.Repository.SQLite.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly SQLiteAsyncConnection _connection;

    public OrderRepository(SQLiteAsyncConnection connection)
    {
        _connection = connection;
    }

    public IEnumerable<Order> GetAll()
    {
        return _connection.Table<Order>().ToListAsync().Result;
    }

    public void Save(Order order)
    {
        _connection.InsertAsync(order).Wait();
    }
}