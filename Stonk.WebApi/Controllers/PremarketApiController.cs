using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stonk.Application.Contracts.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stonk.WebApi.Controllers
{
    public class PremarketApiController : StonkBaseApiController
    {
        private readonly IPremarketStockService _service;
        private readonly IStonkInformationService _servicev2;
        private readonly ILogger<PremarketApiController> _logger;

        public PremarketApiController(IPremarketStockService service,
            ILogger<PremarketApiController> logger,
            IStonkInformationService serviceV2)
        {
            _service = service;
            _servicev2 = serviceV2;
            _logger = logger;
        }

        [HttpGet("premarketdata/{symbol}")]
        public async Task<IActionResult> GetPremarketData(string symbol)
        {
            //var result = await _service.FetchPremarketData(symbol);

            var result = await _servicev2.GetStockInfos(symbol);

            return HandleResponse(result);
        }
    }
}
