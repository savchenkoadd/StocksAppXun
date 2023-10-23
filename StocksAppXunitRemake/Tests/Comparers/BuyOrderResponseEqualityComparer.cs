using ServiceContracts.DTO.BuyOrder;

namespace Tests.Comparers
{
	public class BuyOrderResponseEqualityComparer : IEqualityComparer<BuyOrderResponse>
	{
		public bool Equals(BuyOrderResponse x, BuyOrderResponse y)
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

		public int GetHashCode(BuyOrderResponse obj)
		{
			return obj.GetHashCode();
		}
	}
}
