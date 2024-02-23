using HtmlAgilityPack;

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
                    var tbodyNodes = doc.DocumentNode.SelectNodes("//div[@class='table-responsive inner-scroll']//tbody");
                    if (tbodyNodes != null)
                    {
                        foreach (var tbodyNode in tbodyNodes)
                        {
                            var stockNodes = tbodyNode.SelectNodes(".//tr");
                            if (stockNodes != null)
                            {
                                foreach (var stockNode in stockNodes)
                                {
                                    var tradeCode = stockNode.SelectSingleNode(".//td[2]/a")?.InnerText.Trim();
                                    var lastTradingPrice = stockNode.SelectSingleNode(".//td[3]")?.InnerText.Trim();
                                    var high = stockNode.SelectSingleNode(".//td[4]")?.InnerText.Trim();
                                    var low = stockNode.SelectSingleNode(".//td[5]")?.InnerText.Trim();
                                    var closePrice = stockNode.SelectSingleNode(".//td[6]")?.InnerText.Trim();
                                    var yesterdayClosePrice = stockNode.SelectSingleNode(".//td[7]")?.InnerText.Trim();
                                    var change = stockNode.SelectSingleNode(".//td[8]")?.InnerText.Trim();
                                    var trade = stockNode.SelectSingleNode(".//td[9]")?.InnerText.Trim();
                                    var value = stockNode.SelectSingleNode(".//td[10]")?.InnerText.Trim();
                                    var volume = stockNode.SelectSingleNode(".//td[11]")?.InnerText.Trim();

                                    _logger.LogInformation("TradeCode: {0}, LastTradingPrice: {1}, High: {2}, Low: {3}, ClosePrice: {4}, YesterdayClosePrice: {5}, Change: {6}, Trade: {7}, Value: {8}, Volume: {9}",
                                        tradeCode, lastTradingPrice, high, low, closePrice, yesterdayClosePrice, change, trade, value, volume);
                                }
                            }
                        }
                    }
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
