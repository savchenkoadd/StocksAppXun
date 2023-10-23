using System.ComponentModel.DataAnnotations;
using ServiceContracts.DTO.CustomAttributes;

namespace ServiceContracts.DTO.BuyOrder
{
	public class BuyOrderRequest
	{
		[Required(ErrorMessage = "StockSymbol cannot be null")]
		public string? StockSymbol { get; set; }

		[Required(ErrorMessage = "StockName cannot be null")]
		public string? StockName { get; set; }

		[CustomDateTimeRange("2000-01-01", "2050-01-01",
			ErrorMessage = "Order DateTime cannot be less than 2000 years or larger than 2050 years")]
		public DateTime OrderDateTime { get; set; }

		[Range(1, 100000, ErrorMessage = "Quantity has to be between 1 and 100000")]
		public uint Quantity { get; set; }

		[Range(1, 10000, ErrorMessage = "Price has to be between 1 and 10000")]
		public double Price { get; set; }
	}
}
