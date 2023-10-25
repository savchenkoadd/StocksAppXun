using ServiceContracts.DTO.BuyOrder;
using ServiceContracts.DTO.SellOrder;

namespace StocksApp.ViewModels
{
	public class Orders
	{
		public List<BuyOrderResponse> BuyOrders { get; set; }
		public List<SellOrderResponse> SellOrders { get; set; }
	}
}
