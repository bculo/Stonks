using Stonk.Application.Dtos.Premarket;
using Stonk.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stonk.Application.Contracts.Modules
{
    public interface IPremarketService
    {
        Task<Result<PremarketDataResponseDto>> FetchPremarketData(string symbol);
    }
}
