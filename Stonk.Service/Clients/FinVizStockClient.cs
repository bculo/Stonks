using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stonk.Application.Contracts.Clients;
using Stonk.Application.Dtos.Stonks.Client;
using Stonk.Application.Extensions;
using Stonk.Application.Models;
using Stonk.Application.Options;
using Stonk.Service.Clients.Common;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stonk.Service.Clients
{
    public class FinVizStockClient : HttpBaseClient, IStonkInfoClient
    {
        public FinVizStockClient(HttpClient http,
            IOptions<FinVizOptions> options,
            ILogger<FinVizStockClient> logger)
            : base(http, options.Value, logger)
        {

        }


        public Task<Result<StockInfoClientResponseDto>> GetStockInfo(string symbol)
        {
            return Task.FromResult(ResultExtensions.Success(new StockInfoClientResponseDto { }));
        }
    }
}
