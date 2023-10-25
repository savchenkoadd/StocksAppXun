using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Options;
using ServiceContracts;
using StocksApp.ViewModels;
using ServiceContracts.DTO.SellOrder;
using ServiceContracts.DTO.BuyOrder;

namespace StocksApp.Controllers
{
	[Route("[controller]")]
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

		[Route("/")]
		[Route("[action]")]
		public async Task<IActionResult> Index(string? ticker)
		{
			if (ticker is null)
			{
				return View(await ParseStockTrade(_finnhubOptions.DefaultStockSymbol));
			}

			return View(await ParseStockTrade(ticker));
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> SellOrder(StockTrade stockTrade)
		{
			await _stocksService.CreateSellOrder(new SellOrderRequest() { DateAndTimeOfOrder = DateTime.Now, Price = stockTrade.Price, Quantity = stockTrade.Quantity, StockName = stockTrade.StockName, StockSymbol = stockTrade.StockSymbol });

			return RedirectToAction("Orders");
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> BuyOrder(StockTrade stockTrade)
		{
			await _stocksService.CreateBuyOrder(new BuyOrderRequest() { OrderDateTime = DateTime.Now, Price = stockTrade.Price, Quantity = stockTrade.Quantity, StockName = stockTrade.StockName, StockSymbol = stockTrade.StockSymbol });

			var list = await _stocksService.GetBuyOrders();

			return RedirectToAction("Orders");
		}

		[Route("[action]")]
		public async Task<IActionResult> Orders()
		{
			Orders orders = new Orders()
			{
				BuyOrders = await _stocksService.GetBuyOrders(),
				SellOrders = await _stocksService.GetSellOrders()
			};

			return View(orders);
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
