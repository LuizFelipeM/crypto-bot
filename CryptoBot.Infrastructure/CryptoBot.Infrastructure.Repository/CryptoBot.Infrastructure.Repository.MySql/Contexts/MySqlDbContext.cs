using CryptoBot.Domain;
using CryptoBot.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoBot.Infrastructure.Repository.MySql.Contexts;

public class MySqlDbContext : DbContext
{
    public DbSet<KlineEntity> Klines { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
    {
    }
}
