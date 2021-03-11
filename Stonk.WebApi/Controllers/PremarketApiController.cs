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
        private readonly IPremarketService _service;
        private readonly ILogger<PremarketApiController> _logger;

        public PremarketApiController(IPremarketService service, ILogger<PremarketApiController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("premarketdata/{symbol}")]
        public async Task<IActionResult> GetPremarketData(string symbol)
        {
            var result = await _service.FetchPremarketData(symbol);

            return HandleResponse(result);
        }
    }
}
