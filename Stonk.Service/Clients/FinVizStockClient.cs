using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stonk.Application.Contracts.Clients;
using Stonk.Application.Contracts.Services;
using Stonk.Application.Dtos.Stonks.Client;
using Stonk.Application.Extensions;
using Stonk.Application.Models;
using Stonk.Application.Options;
using Stonk.Service.Clients.Common;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stonk.Service.Clients
{
    /// <summary>
    /// Client for Finviz site
    /// </summary>
    public class FinVizStockClient : HttpBaseClient, IStonkInfoClient
    {
        private readonly IHtmlLookupService _lookup;

        public FinVizStockClient(HttpClient http,
            IOptions<FinVizOptions> options,
            ILogger<FinVizStockClient> logger,
            IHtmlLookupService lookup)
            : base(http, options.Value, logger)
        {
            _lookup = lookup;
        }

        /// <summary>
        /// Fetch stock information and data
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public async Task<Result<StockInfoClientResponseDto>> GetStockInfo(string symbol)
        {
            _logger.LogInformation("Fetching data for ticker {0}", symbol);

            AddQueryParam("t", symbol);

            var response = await _http.GetAsync(BuildQuery());

            _logger.LogInformation("Data fetched for ticker {0}", symbol);

            var result = await HandleClientResponse(response);

            if (!result.Succedded)
            {
                _logger.LogInformation("Fetchind data for ticker {0} failed -> {1}", symbol, result.Message);
                return ResultExtensions.Failure<StockInfoClientResponseDto>(result.Message);
            }

            if (!ContainsTickerId(result.Instance, symbol))
            {
                _logger.LogInformation("Ticker {0} not found", symbol);
                return ResultExtensions.Failure<StockInfoClientResponseDto>("Ticker not found");
            }

            var fetchedInstance = ParseInstanceFromHtmlContent(result.Instance);

            _logger.LogInformation("Data for ticker fetched and prepared");

            return ResultExtensions.Success(fetchedInstance);
        }

        /// <summary>
        /// Create StockInfoClientResponseDto instance using html content
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private StockInfoClientResponseDto ParseInstanceFromHtmlContent(string html)
        {
            var dto = new StockInfoClientResponseDto { };

            _logger.LogInformation("Filling response instance with data from HTML");

            FillSourceInformation(dto);

            FillBasicStockInfo(html, dto);

            FillAnalyticData(html, dto);

            _logger.LogInformation("Response instances filled");

            return dto;
        }

        /// <summary>
        /// Fill instnace with data source information
        /// </summary>
        /// <param name="dto"></param>
        private void FillSourceInformation(StockInfoClientResponseDto dto)
        {
            dto.PageSource = "finviz";
            dto.PageSourceUrl = _url.Url;
        }

        /// <summary>
        /// Fill instance with stock symbol, stock name, country origin etc.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="dto"></param>
        private void FillBasicStockInfo(string html, StockInfoClientResponseDto dto)
        {
            _logger.LogInformation("Filling basic info about given ticker");

            dto.Symbol = _lookup.FindHtmlElementById(html, "ticker", true);

            var htmlSection = _lookup.FindHtmlElementByClass(html, "fullview-title");

            var fullNameHtmlSection = _lookup.FindHtmlElementByTag(htmlSection, "tr", 1); // --> fullname table row
            dto.Name = _lookup.FindHtmlElementByTag(fullNameHtmlSection, "b", true);

            var sectorDetailsHtmlSection = _lookup.FindHtmlElementByTag(htmlSection, "tr", 2); // --> sector table row
            dto.Sector = _lookup.FindHtmlElementByTag(sectorDetailsHtmlSection, "a", 0, true);
            dto.SubSector = _lookup.FindHtmlElementByTag(sectorDetailsHtmlSection, "a", 1, true);
            dto.Country = _lookup.FindHtmlElementByTag(sectorDetailsHtmlSection, "a", 2, true);

            dto.Description = _lookup.FindHtmlElementByClass(html, "fullview-profile", true);

            _logger.LogInformation("Instance filled with basic info");
        }

        /// <summary>
        /// Fill instance with analytic data like market price, prediction price, price movment and so on
        /// </summary>
        /// <param name="html"></param>
        /// <param name="dto"></param>
        private void FillAnalyticData(string html, StockInfoClientResponseDto dto)
        {
            _logger.LogInformation("Filling instance with analytic data");

            var snapShotSection = _lookup.FindHtmlElementByClass(html, "snapshot-table2"); // table details for given ticker

            var firstRowSection = _lookup.FindHtmlElementByTag(snapShotSection, "tr", 0); // --> First table row
            dto.InsiderOwn = _lookup.FindHtmlElementByTag(firstRowSection, "b", 3, true);
            dto.SharesOutstand = _lookup.FindHtmlElementByTag(firstRowSection, "b", 4, true);

            var secondRowSection = _lookup.FindHtmlElementByTag(snapShotSection, "tr", 1); // --> Second table row
            dto.MarcketCap = _lookup.FindHtmlElementByTag(secondRowSection, "b", 0, true);
            dto.InsiderTransition = _lookup.FindHtmlElementByTag(secondRowSection, "b", 3, true);
            dto.SharesFloat = _lookup.FindHtmlElementByTag(secondRowSection, "b", 4, true);

            var thirdRowSection = _lookup.FindHtmlElementByTag(snapShotSection, "tr", 2); // --> Third table row
            dto.Income = _lookup.FindHtmlElementByTag(thirdRowSection, "b", 0, true);
            dto.InstitutionOwn = _lookup.FindHtmlElementByTag(thirdRowSection, "b", 3, true);
            dto.SharesFloat = _lookup.FindHtmlElementByTag(thirdRowSection, "b", 4, true);

            var fourthRowSection = _lookup.FindHtmlElementByTag(snapShotSection, "tr", 3); // --> Fourth table row
            dto.Sales = _lookup.FindHtmlElementByTag(fourthRowSection, "b", 0, true);
            dto.InstitutionTransition = _lookup.FindHtmlElementByTag(fourthRowSection, "b", 3, true);

            var fifthRowSection = _lookup.FindHtmlElementByTag(snapShotSection, "tr", 4); // --> Fifth table row
            dto.TargetPrice = _lookup.FindHtmlElementByTag(fifthRowSection, "b", 4, true);

            var rowNineSection = _lookup.FindHtmlElementByTag(snapShotSection, "tr", 9); // --> 9. table row
            dto.PreviousPrice = _lookup.FindHtmlElementByTag(rowNineSection, "b", 5, true);

            var rowTenSection = _lookup.FindHtmlElementByTag(snapShotSection, "tr", 10); // --> 10. table row
            dto.AverageVolume = _lookup.FindHtmlElementByTag(rowTenSection, "b", 4, true);
            dto.Price = _lookup.FindHtmlElementByTag(rowTenSection, "b", 5, true);

            var rowElevenSection = _lookup.FindHtmlElementByTag(snapShotSection, "tr", 11); // --> 11. table row
            dto.Volume = _lookup.FindHtmlElementByTag(rowElevenSection, "b", 4, true);

            _logger.LogInformation("Instance filled with analytic data");
        }

        /// <summary>
        /// Check if html content containts given symbol (stonk successfuly fetched?)
        /// </summary>
        /// <param name="html"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private bool ContainsTickerId(string html, string symbol)
        {
            if (html.Contains(symbol, System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}
