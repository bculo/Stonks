using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stonk.Application.Contracts.Clients;
using Stonk.Application.Contracts.Services;
using Stonk.Application.Dtos.Stonks.Client;
using Stonk.Application.Models;
using Stonk.Application.Options;
using Stonk.Service.Clients.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Stonk.Service.Clients
{
    public class YahooFinanceClient : HttpBaseClient, IStonkInfoClient
    {
        private readonly IHtmlLookupService _lookup;

        public YahooFinanceClient(HttpClient http,
            IOptions<YahooFinanceOptions> options,
            ILogger<YahooFinanceClient> logger,
            IHtmlLookupService lookup)
            : base(http, options.Value, logger)
        {
            _lookup = lookup;
        }

        public Task<Result<StockInfoClientResponseDto>> GetStockInfo(string symbol)
        {
            throw new NotImplementedException();
        }
    }
}
