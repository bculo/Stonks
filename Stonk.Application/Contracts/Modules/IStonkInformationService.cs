using Stonk.Application.Dtos.Stonks;
using Stonk.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stonk.Application.Contracts.Modules
{
    public interface IStonkInformationService
    {
        public Task<ResultArray<StockInfoResponseDto>> GetStockInfos(string symbol);
        public Task<Result<StockInfoResponseDto>> GetStockInfo(StockInfoRequestDto request);
    }
}
