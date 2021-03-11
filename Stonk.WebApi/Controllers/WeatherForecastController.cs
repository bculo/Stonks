using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stonk.Application.Contracts.Clients;
using Stonk.Application.Contracts.Modules;
using Stonk.Application.Contracts.Services;
using Stonk.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stonk.WebApi.Controllers
{
    public class WeatherForecastController : StonkBaseApiController
    {
        

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IPremarketService _client;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IPremarketService client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpGet]
        public async  Task<IActionResult> Get()
        {
            var result = await _client.FetchPremarketData("bngo");

            return Ok(result.Instance);
        }
    }
}
