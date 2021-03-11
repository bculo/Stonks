using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stonk.Application.Contracts.Clients;
using Stonk.Application.Models;
using Stonk.Application.Options;
using Stonk.Core;
using Stonk.Core.Enum;
using Stonk.Service.Clients.Common;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stonk.Service.Clients
{
    public class MarketWatchClient : HttpBaseClient, IPremarketClient
    {
        public MarketWatchClient(HttpClient http, 
            IOptions<PremarketOptions> options, 
            ILogger<MarketWatchClient> logger) 
            : base(http, options.Value, logger)
        {

        }

        public async Task<Result<string>> GetPremarketData(string symbol, StonkType type)
        {
            _logger.LogInformation("Fetching premarket data for symbol {0}", symbol);

            var response = await _http.GetAsync($"{type}/{symbol}");

            _logger.LogInformation("Premarket data fetched for symbol {0}", symbol);

            return await HandleClientResponse(response);
        }
    }
}
