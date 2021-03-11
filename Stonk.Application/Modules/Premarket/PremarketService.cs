using Microsoft.Extensions.Logging;
using Stonk.Application.Contracts.Clients;
using Stonk.Application.Contracts.Modules;
using Stonk.Application.Contracts.Services;
using Stonk.Application.Dtos.Premarket;
using Stonk.Application.Extensions;
using Stonk.Application.Models;
using Stonk.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stonk.Application.Modules.Premarket
{
    public class PremarketService : IPremarketService
    {
        private readonly ILogger<PremarketService> _logger;
        private readonly IPremarketClient _client;
        private readonly IHtmlLookupService _lookup;

        public PremarketService(ILogger<PremarketService> logger,
            IPremarketClient client,
            IHtmlLookupService lookup)
        {
            _logger = logger;
            _lookup = lookup;
            _client = client;
        }

        public async Task<Result<PremarketDataResponseDto>> FetchPremarketData(string symbol)
        {
            var result = await _client.GetPremarketData(symbol, StonkType.Stock);

            if (result.Succedded)
            {
                var instance = CreateInstanceBasedOnHtmlContent(result.Instance);
                return ResultExtensions.Success(instance);
            }

            return ResultExtensions.Failure<PremarketDataResponseDto>("Stock with given symbol not found");
        }

        public PremarketDataResponseDto CreateInstanceBasedOnHtmlContent(string html)
        {
            var dto = new PremarketDataResponseDto { };

            var htmlElement = _lookup.FindHtmlElementByClass(html, "element--intraday");

            dto.Symbol = _lookup.FindHtmlElementByClass(htmlElement, "company__ticker", true);
            dto.Market = _lookup.FindHtmlElementByClass(htmlElement, "company__market", true);

            var priceSectionElement = _lookup.FindHtmlElementByClass(htmlElement, "intraday__price");

            dto.Price = _lookup.FindHtmlElementByClass(priceSectionElement, "value", true);

            return dto;
        }
    }
}
