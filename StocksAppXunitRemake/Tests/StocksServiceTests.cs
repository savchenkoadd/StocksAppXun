using ServiceContracts.DTO;
using ServiceContracts;
using Services;
using ServiceContracts.DTO.BuyOrder;
using ServiceContracts.DTO.SellOrder;
using Tests.Comparers;

namespace Tests
{
	public class StocksServiceTests
	{
		private readonly IStocksService _stocksService;

		public StocksServiceTests()
		{
			_stocksService = new StocksService();
		}

		#region CreateBuyOrder 

		#region TestCases

		//When you supply BuyOrderRequest as null, it should throw ArgumentNullException.
		[Fact]
		public async Task CreateBuyOrder_NullBuyOrderRequest()
		{
			await CreateBuyOrderAssertThrowsAsync<ArgumentNullException>(null!);
		}

		//When you supply buyOrderQuantity as 0 (as per the specification, minimum is 1), it should throw ArgumentException. When you supply buyOrderQuantity as 100001 (as per the specification, maximum is 100000), it should throw ArgumentException.
		[Fact]
		public async Task CreateBuyOrder_InvalidQuantity()
		{
			foreach (var item in GetInvalidQuantityBuyTestRequests())
			{
				await CreateBuyOrderAssertThrowsAsync<ArgumentException>(item);
			}
		}

		/*When you supply buyOrderPrice as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
		  When you supply buyOrderPrice as 10001 (as per the specification, maximum is 10000), it should throw ArgumentException.*/
		[Fact]
		public async Task CreateBuyOrder_InvalidPrice()
		{
			foreach (var testRequest in GetInvalidPriceBuyTestRequests())
			{
				await CreateBuyOrderAssertThrowsAsync<ArgumentException>(testRequest);
			}
		}

		//When you supply stock symbol=null (as per the specification, stock symbol can't be null), it should throw ArgumentException.
		[Fact]
		public async Task CreateBuyOrder_NullStockSymbol()
		{
			await CreateBuyOrderAssertThrowsAsync<ArgumentException>(new BuyOrderRequest() { StockSymbol = null! });
		}

		//When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
		[Fact]
		public async Task CreateBuyOrder_InvalidOrderDateTime()
		{
			await CreateBuyOrderAssertThrowsAsync<ArgumentException>(new BuyOrderRequest() { OrderDateTime = new DateTime(1999, 12, 31) });
		}

		//If you supply all valid values, it should be successful and return an object of BuyOrderResponse type with auto-generated BuyOrderID (guid)
		[Fact]
		public async Task CreateBuyOrder_ValidData()
		{
			var response = await _stocksService.CreateBuyOrder(CreateValidTestBuyRequest());

			Assert.NotNull(response);

			AssertValidGuid(response);
		}

		#endregion

		#region Additional Methods

		private async Task CreateBuyOrderAssertThrowsAsync<TException>(BuyOrderRequest request) where TException : Exception
		{
			await AssertThrowsAsync<TException>(() => _stocksService.CreateBuyOrder(request));
		}

		private List<BuyOrderRequest> GetInvalidQuantityBuyTestRequests()
		{
			return new List<BuyOrderRequest>()
			{
				new BuyOrderRequest() { Quantity = 0 },
				new BuyOrderRequest() { Quantity = 100001 },
			};
		}

		private List<BuyOrderRequest> GetInvalidPriceBuyTestRequests()
		{
			return new List<BuyOrderRequest>()
			{
				new BuyOrderRequest() { Price = 0 },
				new BuyOrderRequest() { Price = -1 },
				new BuyOrderRequest() { Price = 10001 }
			};
		}

		private BuyOrderRequest CreateValidTestBuyRequest()
		{
			return new BuyOrderRequest()
			{
				OrderDateTime = new DateTime(2011, 11, 25),
				Price = 2000,
				Quantity = 10,
				StockSymbol = "AAPL",
				StockName = "Apple Inc"
			};
		}

		#endregion

		#endregion

		#region CreateSellOrder

		#region TestCases

		//When you supply SellOrderRequest as null, it should throw ArgumentNullException.
		[Fact]
		public async Task CreateSellOrder_NullSellOrderRequest()
		{
			await CreateSellOrderAssertThrowsAsync<ArgumentNullException>(null!);
		}

		//When you supply sellOrderQuantity as 0 (as per the specification, minimum is 1), it should throw ArgumentException. When you supply sellOrderQuantity as 100001 (as per the specification, maximum is 100000), it should throw ArgumentException.
		[Fact]
		public async Task CreateSellOrder_InvalidQuantity()
		{
			foreach (var item in GetInvalidQuantitySellTestRequests())
			{
				await CreateSellOrderAssertThrowsAsync<ArgumentException>(item);
			}
		}

		//When you supply sellOrderPrice as 0 (as per the specification, minimum is 1), it should throw ArgumentException. When you supply sellOrderPrice as 10001 (as per the specification, maximum is 10000), it should throw ArgumentException.
		[Fact]
		public async Task CreateSellOrder_InvalidPrice()
		{
			foreach (var item in GetInvalidPriceSellTestRequests())
			{
				await CreateSellOrderAssertThrowsAsync<ArgumentException>(item);
			}
		}

		//When you supply stock symbol=null (as per the specification, stock symbol can't be null), it should throw ArgumentException.
		[Fact]
		public async Task CreateSellOrder_NullStockSymbol()
		{
			await CreateSellOrderAssertThrowsAsync<ArgumentException>(new SellOrderRequest() { StockSymbol = null! });
		}

		//When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
		[Fact]
		public async Task CreateSellOrder_InvalidOrderDateTime()
		{
			await CreateSellOrderAssertThrowsAsync<ArgumentException>(new SellOrderRequest() { DateAndTimeOfOrder = new DateTime(1999, 12, 31) });
		}

		//If you supply all valid values, it should be successful and return an object of SellOrderResponse type with auto-generated SellOrderID (guid).
		[Fact]
		public async Task CreateSellOrder_Valid()
		{
			var response = await _stocksService.CreateSellOrder(CreateValidTestSellRequest());

			Assert.NotNull(response);

			AssertValidGuid(response);
		}

		#endregion

		#region AdditionalMethods

		private async Task CreateSellOrderAssertThrowsAsync<TException>(SellOrderRequest request) where TException : Exception
		{
			await AssertThrowsAsync<TException>(() => _stocksService.CreateSellOrder(request));
		}

		private List<SellOrderRequest> GetInvalidQuantitySellTestRequests()
		{
			return new List<SellOrderRequest>()
			{
				new SellOrderRequest() { Quantity = 0 },
				new SellOrderRequest() { Quantity = 100001 },
			};
		}

		private List<SellOrderRequest> GetInvalidPriceSellTestRequests()
		{
			return new List<SellOrderRequest>()
			{
				new SellOrderRequest() { Price = 0 },
				new SellOrderRequest() { Price = -1 },
				new SellOrderRequest() { Price = 10001 }
			};
		}

		private SellOrderRequest CreateValidTestSellRequest()
		{
			return new SellOrderRequest()
			{
				Quantity = 12,
				DateAndTimeOfOrder = DateTime.Today,
				Price = 10000,
				StockName = "Apple Inc",
				StockSymbol = "AAPL"
			};
		}

		#endregion

		#endregion

		#region GetBuyOrders

		//When you invoke this method, by default, the returned list should be empty.
		[Fact]
		public async Task GetBuyOrders_EmptyList()
		{
			List<BuyOrderResponse> orders = await _stocksService.GetBuyOrders();

			Assert.Empty(orders);
		}

		//When you first add few buy orders using CreateBuyOrder() method; and then invoke GetAllBuyOrders() method; the returned list should contain all the same buy orders.
		[Fact]
		public async Task GetBuyOrders_Valid()
		{
			await AddFewBuyTestOrders();

			List<BuyOrderResponse> actual = await _stocksService.GetBuyOrders();

			List<BuyOrderResponse> expected = new List<BuyOrderResponse>()
			{
				new BuyOrderResponse()
				{
					DateAndTimeOfOrder = DateTime.Today,
					Price = 1342,
					Quantity = 5,
					StockName = "Microsoft",
					StockSymbol = "MSFT"
				},
				new BuyOrderResponse()
				{
					DateAndTimeOfOrder = DateTime.Today,
					Quantity = 13,
					Price = 2342,
					StockName = "Apple Inc",
					StockSymbol = "AAPL"
				}
			};

			Assert.Equal(expected, actual, new BuyOrderResponseEqualityComparer());
		}

		#region AdditionalMethods

		private async Task AddFewBuyTestOrders()
		{
			await _stocksService.CreateBuyOrder(
				new BuyOrderRequest()
				{
					OrderDateTime = DateTime.Today,
					Quantity = 5,
					Price = 1342,
					StockName = "Microsoft",
					StockSymbol = "MSFT"
				});

			await _stocksService.CreateBuyOrder(
				new BuyOrderRequest()
				{
					OrderDateTime = DateTime.Today,
					Quantity = 13,
					Price = 2342,
					StockName = "Apple Inc",
					StockSymbol = "AAPL"
				});
		}

		#endregion

		#endregion

		#region GetSellOrders

		//When you invoke this method, by default, the returned list should be empty.
		[Fact]
		public async Task GetSellOrders_EmptyList()
		{
			List<SellOrderResponse> orders = await _stocksService.GetSellOrders();

			Assert.Empty(orders);
		}

		//When you first add few buy orders using CreateBuyOrder() method; and then invoke GetAllBuyOrders() method; the returned list should contain all the same buy orders.
		[Fact]
		public async Task GetSellOrders_Valid()
		{
			await AddFewSellTestOrders();

			List<SellOrderResponse> actual = await _stocksService.GetSellOrders();

			List<SellOrderResponse> expected = new List<SellOrderResponse>()
			{
				new SellOrderResponse()
				{
					DateAndTimeOfOrder = DateTime.Today,
					Price = 1342,
					Quantity = 5,
					StockName = "Microsoft",
					StockSymbol = "MSFT"
				},
				new SellOrderResponse()
				{
					DateAndTimeOfOrder = DateTime.Today,
					Quantity = 13,
					Price = 2342,
					StockName = "Apple Inc",
					StockSymbol = "AAPL"
				}
			};

			Assert.Equal(expected, actual, new SellOrderResponseEqualityComparer());
		}

		#region AdditionalMethods

		private async Task AddFewSellTestOrders()
		{
			await _stocksService.CreateSellOrder(
				new SellOrderRequest()
				{
					DateAndTimeOfOrder = DateTime.Today,
					Quantity = 5,
					Price = 1342,
					StockName = "Microsoft",
					StockSymbol = "MSFT"
				});

			await _stocksService.CreateSellOrder(
				new SellOrderRequest()
				{
					DateAndTimeOfOrder = DateTime.Today,
					Quantity = 13,
					Price = 2342,
					StockName = "Apple Inc",
					StockSymbol = "AAPL"
				});
		}

		#endregion

		#endregion

		#region AdditionalMethods 

		private async Task AssertThrowsAsync<TException>(Func<Task> action) where TException : Exception
		{
			await Assert.ThrowsAsync<TException>(async () => await action());
		}

		private void AssertValidGuid(OrderResponse response)
		{
			Assert.True(Guid.TryParse(response.OrderId.ToString(), out _));
		}

		#endregion
	}
}
