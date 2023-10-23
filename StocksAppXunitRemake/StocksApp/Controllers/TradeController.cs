using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Options;
using ServiceContracts;
using StocksApp.ViewModels;

namespace StocksApp.Controllers
{
	public class TradeController : Controller
	{
		private readonly FinnhubOptions _finnhubOptions;
		private readonly IStocksService _stocksService;
		private readonly IFinnhubService _finnhubService;

		public TradeController(
				IOptions<FinnhubOptions> finnhubOptions,
				IStocksService stocksService,
				IFinnhubService finnhubService
			)
		{
			_finnhubOptions = finnhubOptions.Value;
			_stocksService = stocksService;
			_finnhubService = finnhubService;
		}

		[Route("{ticker?}")]
		public async Task<IActionResult> StockPage(string? ticker)
		{
			if (ticker is null)
			{
				return View(await ParseStockTrade(_finnhubOptions.DefaultStockSymbol));
			}

			return View(await ParseStockTrade(ticker));
		}

		private async Task<StockTrade> ParseStockTrade(string stockSymbol)
		{
			var quote = await _finnhubService.GetStockPriceQuote(stockSymbol);
			var companyProfile = await _finnhubService.GetCompanyProfile(stockSymbol);

			if (quote is null || companyProfile is null)
			{
				throw new ArgumentNullException("There are some troubles with connection..");
			}

			StockTrade result = new StockTrade()
			{
				Price = Convert.ToDouble(quote["c"].ToString()),
				StockName = companyProfile["name"].ToString(),
				StockSymbol = companyProfile["ticker"].ToString()
			};

			return await Task.FromResult(result);
		}
	}
}
