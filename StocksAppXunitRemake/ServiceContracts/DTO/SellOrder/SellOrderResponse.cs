using ServiceContracts.DTO.BuyOrder;

namespace ServiceContracts.DTO.SellOrder
{
	public class SellOrderResponse : OrderResponse
	{
		public string? StockSymbol { get; set; }
		public string? StockName { get; set; }
		public DateTime DateAndTimeOfOrder { get; set; }
		public uint Quantity { get; set; }
		public double Price { get; set; }
	}

	public static class SellOrderExtensions
	{
		public static SellOrderResponse ToSellOrderResponse(this Entities.SellOrder sellOrder)
		{
			return new SellOrderResponse()
			{
				DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
				Quantity = sellOrder.Quantity,
				Price = sellOrder.Price,
				StockSymbol = sellOrder.StockSymbol,
				StockName = sellOrder.StockName,
				OrderId = sellOrder.SellOrderId
			};
		}
	}
}
