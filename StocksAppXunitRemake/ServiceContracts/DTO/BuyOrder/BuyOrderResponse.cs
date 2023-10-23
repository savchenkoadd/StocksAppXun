namespace ServiceContracts.DTO.BuyOrder
{
	public class BuyOrderResponse : OrderResponse
	{
		public string? StockSymbol { get; set; }
		public string? StockName { get; set; }
		public DateTime DateAndTimeOfOrder { get; set; }
		public uint Quantity { get; set; }
		public double Price { get; set; }
	}

	public static class BuyOrderExtensions
	{
		public static BuyOrderResponse ToBuyOrderResponse(this Entities.BuyOrder buyOrder)
		{
			return new BuyOrderResponse()
			{
				DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
				Quantity = buyOrder.Quantity,
				Price = buyOrder.Price,
				StockSymbol = buyOrder.StockSymbol,
				StockName = buyOrder.StockName,
				OrderId = buyOrder.BuyOrderId
			};
		}
	}
}
