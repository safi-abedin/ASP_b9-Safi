using StockData.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Application.Features.Scrapper
{
    public class StockDataManagementService : IStockDataManagementService
    {
        public IApplicationUnitOfWork _unitOfWork { get; set; }

        public StockDataManagementService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateCompany(string tradingCode)
        {
            var count = await _unitOfWork.CompanyRepository.GetCompanyCountAsync(tradingCode);
            if(count == 0)
            {
                Company newCompany = new Company
                {
                    TradeCode = tradingCode
                };
                await _unitOfWork.CompanyRepository.AddAsync(newCompany);
                await _unitOfWork.SaveAsync();
            }
        }



        public async Task CreateStock(string tradingCode, decimal lastTradingPrice, decimal high,
            decimal low, decimal closePrice, decimal yesterdayClosePrice, decimal change,
            decimal trade, decimal value, decimal volume)
        {
            var result = await _unitOfWork.CompanyRepository.GetCompany(tradingCode);

            var company = result.FirstOrDefault();

            if(company is not null)
            {
                StockPrice newStockPrice = new StockPrice
                {
                    CompanyId = company.Id,
                    LastTradingPrice = lastTradingPrice,
                    High = high,
                    Low = low,
                    ClosePrice = closePrice,
                    YesterdayClosePrice = yesterdayClosePrice,
                    Change = change,
                    Volume = volume,
                    Trade = trade,
                    Value = value
                };
                await _unitOfWork.StockPriceRepository.AddAsync(newStockPrice);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
