using Microsoft.EntityFrameworkCore;
using StockData.Domain.Entities;

namespace StockData.Infrastructure
{
    public interface IApplicationDbContext
    {
        DbSet<StockPrice> StockPrices { get; set; }

        DbSet<Company> Companies { get; set; }
    }
}