using Entities;
using ServiceContracts;
using ServiceContracts.DTO.BuyOrder;
using ServiceContracts.DTO.SellOrder;
using Services.Helpers;

namespace Services
{
	public class StocksService : IStocksService
	{
		private readonly List<BuyOrder> _buyOrders = new List<BuyOrder>();
		private readonly List<SellOrder> _sellOrders = new List<SellOrder>();

		public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
		{
			await ValidateRequest(buyOrderRequest);

			Guid id = Guid.NewGuid();

			_buyOrders.Add(new BuyOrder()
			{
				BuyOrderId = id,
				DateAndTimeOfOrder = buyOrderRequest.OrderDateTime,
				Price = buyOrderRequest.Price,
				Quantity = buyOrderRequest.Quantity,
				StockName = buyOrderRequest.StockName,
				StockSymbol = buyOrderRequest.StockSymbol
			});

			return new BuyOrderResponse()
			{
				OrderId = id,
				StockSymbol = buyOrderRequest.StockSymbol,
				StockName = buyOrderRequest.StockName,
				Quantity = buyOrderRequest.Quantity, 
				Price= buyOrderRequest.Price,
				DateAndTimeOfOrder= buyOrderRequest.OrderDateTime
			};
		}

		public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
		{
			await ValidateRequest(sellOrderRequest);

			Guid id = Guid.NewGuid();

			_sellOrders.Add(new SellOrder()
			{
				SellOrderId = id,
				DateAndTimeOfOrder = sellOrderRequest.DateAndTimeOfOrder,
				Price = sellOrderRequest.Price,
				Quantity = sellOrderRequest.Quantity,
				StockName = sellOrderRequest.StockName,
				StockSymbol = sellOrderRequest.StockSymbol
			});

			return new SellOrderResponse()
			{
				OrderId = id,
				StockSymbol = sellOrderRequest.StockSymbol,
				StockName = sellOrderRequest.StockName,
				Quantity = sellOrderRequest.Quantity,
				Price = sellOrderRequest.Price,
				DateAndTimeOfOrder = sellOrderRequest.DateAndTimeOfOrder
			};
		}

		public async Task<List<BuyOrderResponse>> GetBuyOrders()
		{
			return await Task.Run(() =>
			{
				return _buyOrders.Select(buyOrder => buyOrder.ToBuyOrderResponse()).ToList();
			});
		}

		public async Task<List<SellOrderResponse>> GetSellOrders()
		{
			return await Task.Run(() =>
			{
				return _sellOrders.Select(sellOrder => sellOrder.ToSellOrderResponse()).ToList();
			});
		}

		private async Task ValidateRequest(object? obj)
		{
			await Task.Run(() =>
			{
				var errors = ValidationHelper.GetValidationErrors(obj);

				if (errors.Count > 0)
				{
					throw new ArgumentException(errors?.FirstOrDefault()?.ErrorMessage);
				}
			});
		}
	}
}
