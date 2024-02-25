using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using StockData.Application.Features.Scrapper;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockData.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public IStockDataManagementService _stockDataManagementService { get; set; }

        public Worker(ILogger<Worker> logger,IStockDataManagementService stockDataManagementService)
        {
            _logger = logger;
            _stockDataManagementService = stockDataManagementService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var web = new HtmlWeb();
                var doc = web.Load("https://www.dse.com.bd/latest_share_price_scroll_l.php");

                var marketStatusNode = doc.DocumentNode.SelectSingleNode("//span[@class='time'][contains(text(), 'Market Status')]/span");
                var marketStatus = marketStatusNode?.InnerText.Trim();
                _logger.LogInformation(marketStatus);


                if (marketStatus != null && marketStatus.Contains("Open"))
                {
                    _logger.LogInformation("Market is closed. No data scraping performed.");
                }
                else
                {
                    var data = doc.DocumentNode.SelectNodes("//div[@class='table-responsive inner-scroll']//table//tr");


                    foreach (var row in data)
                    {
                        var TradingCode = row.SelectSingleNode(".//td[2]/a")?.InnerText.Trim();
                        if (TradingCode == null)
                        {
                            continue;
                        }

                        var LastTradingPriceStr = row.SelectSingleNode(".//td[3]")?.InnerText.Trim();
                        var HighStr = row.SelectSingleNode(".//td[4]")?.InnerText.Trim();
                        var LowStr = row.SelectSingleNode(".//td[5]")?.InnerText.Trim();
                        var ClosePriceStr = row.SelectSingleNode(".//td[6]")?.InnerText.Trim();
                        var YesterdayClosePriceStr = row.SelectSingleNode(".//td[7]")?.InnerText.Trim();
                        var ChangeStr = row.SelectSingleNode(".//td[8]")?.InnerText.Trim();
                        var TradeStr = row.SelectSingleNode(".//td[9]")?.InnerText.Trim();
                        var ValueStr = row.SelectSingleNode(".//td[10]")?.InnerText.Trim();
                        var VolumeStr = row.SelectSingleNode(".//td[11]")?.InnerText.Trim();

                        // Convert string values to decimal
                        decimal.TryParse(LastTradingPriceStr, out decimal LastTradingPrice);
                        decimal.TryParse(HighStr, out decimal High);
                        decimal.TryParse(LowStr, out decimal Low);
                        decimal.TryParse(ClosePriceStr, out decimal ClosePrice);
                        decimal.TryParse(YesterdayClosePriceStr, out decimal YesterdayClosePrice);
                        decimal.TryParse(ChangeStr, out decimal Change);
                        decimal.TryParse(TradeStr, out decimal Trade);
                        decimal.TryParse(ValueStr, out decimal Value);
                        decimal.TryParse(VolumeStr, out decimal Volume);

                        await _stockDataManagementService.CreateCompany(TradingCode);

                        await _stockDataManagementService.CreateStock(TradingCode, LastTradingPrice, High, Low
                            , ClosePrice, YesterdayClosePrice, Change, Trade, Value, Volume);

                    }
                }

                await Task.Delay(60000, stoppingToken); 
            }
        }
    }
}
