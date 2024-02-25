using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Application.Features.Scrapper
{
    public interface IStockDataManagementService
    {
        Task CreateCompany(string tradingCode);
        Task CreateStock(string tradingCode, decimal lastTradingPrice, decimal high, decimal low, decimal closePrice,
            decimal yesterdayClosePrice, decimal change, decimal trade, decimal value, decimal volume);
    }
}
