using Microsoft.Extensions.Logging;
using Stonk.Application.Contracts.Clients;
using Stonk.Application.Contracts.Modules;
using Stonk.Application.Contracts.Services;
using Stonk.Application.Dtos.Stonks;
using Stonk.Application.Extensions;
using Stonk.Application.Models;
using Stonk.Core.Enum;
using System.Threading.Tasks;

namespace Stonk.Application.Modules
{
    public class PremarketStockService : IPremarketStockService
    {
        private readonly ILogger<PremarketStockService> _logger;
        private readonly IPremarketClient _client;
        private readonly IHtmlLookupService _lookup;

        public PremarketStockService(ILogger<PremarketStockService> logger,
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

            if (!result.Succedded)
            {
                return ResultExtensions.Failure<PremarketDataResponseDto>("Stock with given symbol not found");
            }

            if (!ValidPage(result.Instance, "intraday__data"))
            {
                return ResultExtensions.Failure<PremarketDataResponseDto>("Stock with given symbol not found");
            }

            var instance = CreateInstanceBasedOnHtmlContent(result.Instance);
            return ResultExtensions.Success(instance);
        }

        public PremarketDataResponseDto CreateInstanceBasedOnHtmlContent(string html)
        {
            var dto = new PremarketDataResponseDto { };

            var htmlElement = _lookup.FindHtmlElementByClass(html, "element--intraday");

            dto.Symbol = _lookup.FindHtmlElementByClass(htmlElement, "company__ticker", true);
            dto.Market = _lookup.FindHtmlElementByClass(htmlElement, "company__market", true);

            var priceSectionElement = _lookup.FindHtmlElementByClass(htmlElement, "intraday__data");

            dto.Price = _lookup.FindHtmlElementByClass(priceSectionElement, "value", true);
            dto.PriceChange = _lookup.FindHtmlElementByClass(priceSectionElement, "change--point--q", true);
            dto.PriceChangePercantage = _lookup.FindHtmlElementByClass(priceSectionElement, "change--percent--q", true);

            var previousCloseElement = _lookup.FindHtmlElementByClass(htmlElement, "intraday__close");

            dto.PreviuosClose = _lookup.FindHtmlElementByClass(previousCloseElement, "table__cell u-semi", true);

            var timeStampSectionElement = _lookup.FindHtmlElementByClass(htmlElement, "timestamp__time");

            dto.LastUpdate = _lookup.FindHtmlElementByTag(timeStampSectionElement, "bg-quote", true);

            return dto;
        }

        private bool ValidPage(string content, string elementIdentificator)
        {
            if (content.Contains(elementIdentificator))
            {
                return true;
            }

            return false;
        }
    }
}
