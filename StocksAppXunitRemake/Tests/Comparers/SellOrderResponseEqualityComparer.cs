using ServiceContracts.DTO.SellOrder;

namespace Tests.Comparers
{
	public class SellOrderResponseEqualityComparer : IEqualityComparer<SellOrderResponse>
	{
		public bool Equals(SellOrderResponse x, SellOrderResponse y)
		{
			if (x == null || y == null)
			{
				return false;
			}

			return x.DateAndTimeOfOrder == y.DateAndTimeOfOrder &&
				   x.Price == y.Price &&
				   x.Quantity == y.Quantity &&
				   x.StockName == y.StockName &&
				   x.StockSymbol == y.StockSymbol;
		}

		public int GetHashCode(SellOrderResponse obj)
		{
			return obj.GetHashCode();
		}
	}
}
