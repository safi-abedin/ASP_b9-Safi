using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
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

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
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
                        var LastTradingPrice = row.SelectSingleNode(".//td[3]")?.InnerText.Trim();
                        var High = row.SelectSingleNode(".//td[4]")?.InnerText.Trim();
                        var Low = row.SelectSingleNode(".//td[5]")?.InnerText.Trim();
                        var ClosePrice = row.SelectSingleNode(".//td[6]")?.InnerText.Trim();
                        var YesterdayClosePrice = row.SelectSingleNode(".//td[7]")?.InnerText.Trim();
                        var Change = row.SelectSingleNode(".//td[8]")?.InnerText.Trim();
                        var Trade = row.SelectSingleNode(".//td[9]")?.InnerText.Trim();
                        var Value = row.SelectSingleNode(".//td[10]")?.InnerText.Trim();
                        var Volume = row.SelectSingleNode(".//td[11]")?.InnerText.Trim();

                        _logger.LogInformation("Trading Code: {TradingCode}, Last Trading Price: {LastTradingPrice}, High: {High}," +
                            " Low: {Low}, Close Price: {ClosePrice}, Yesterday Close Price: {YesterdayClosePrice}, Change: {Change}, Trade: {Trade}, " +
                            "Value: {Value}, Volume: {Volume}", TradingCode, LastTradingPrice, High, Low, ClosePrice, YesterdayClosePrice, Change, Trade, Value, Volume);
                    }
                }

                await Task.Delay(60000, stoppingToken); // Delay for 1 minute before scraping next data
            }
        }
    }
}
