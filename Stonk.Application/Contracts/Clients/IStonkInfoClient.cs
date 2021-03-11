using Stonk.Application.Dtos.Stonks;
using Stonk.Application.Dtos.Stonks.Client;
using Stonk.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stonk.Application.Contracts.Clients
{
    public interface IStonkInfoClient
    {
        Task<Result<StockInfoClientResponseDto>> GetStockInfo(string symbol);
    }
}
