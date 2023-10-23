using ServiceContracts;
using Options;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Services
{
	public class FinnhubService : IFinnhubService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly FinnhubOptions _options;

		public FinnhubService(IHttpClientFactory httpClientFactory,
			IOptions<FinnhubOptions> options)
		{
			_httpClientFactory = httpClientFactory;
			_options = options.Value;
		}

		public async Task<Dictionary<string, object>?> GetCompanyProfile(string? stockSymbol = null!)
		{
			var response = await GetFromFinnhubAsync("https://finnhub.io/api/v1/stock/profile2", stockSymbol!);

			return await ConvertToDictionary(response);
		}

		public async Task<Dictionary<string, object>?> GetStockPriceQuote(string? stockSymbol = null!)
		{
			var response = await GetFromFinnhubAsync("https://finnhub.io/api/v1/quote", stockSymbol);

			return await ConvertToDictionary(response);
		}

		private async Task<Dictionary<string, object>?> ConvertToDictionary(HttpResponseMessage response)
		{
			return await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
		}

		private async Task<HttpResponseMessage> GetFromFinnhubAsync(string link, string? stockSymbol)
		{
			using (HttpClient httpClient = _httpClientFactory.CreateClient())
			{
				HttpRequestMessage requestMessage;

				if (string.IsNullOrEmpty(stockSymbol))
				{
					requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{link}?symbol={_options.DefaultStockSymbol}&token={_options.ApiKey}");
				}
				else
				{
					requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{link}?symbol={stockSymbol}&token={_options.ApiKey}");
				}

				return await httpClient.SendAsync(requestMessage);
			}
		}
	}
}