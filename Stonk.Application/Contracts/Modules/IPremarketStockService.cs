using Stonk.Application.Dtos.Stonks;
using Stonk.Application.Models;
using System.Threading.Tasks;

namespace Stonk.Application.Contracts.Modules
{
    public interface IPremarketStockService
    {
        Task<Result<PremarketDataResponseDto>> FetchPremarketData(string symbol);
    }
}
