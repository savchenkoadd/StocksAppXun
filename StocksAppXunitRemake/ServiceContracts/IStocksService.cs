using ServiceContracts.DTO.BuyOrder;
using ServiceContracts.DTO.SellOrder;

namespace ServiceContracts
{
	public interface IStocksService
	{
		Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);

		Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);

		Task<List<BuyOrderResponse>> GetBuyOrders();

		Task<List<SellOrderResponse>> GetSellOrders();
	}
}
