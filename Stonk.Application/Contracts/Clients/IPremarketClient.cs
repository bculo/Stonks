using Stonk.Application.Models;
using Stonk.Core;
using Stonk.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stonk.Application.Contracts.Clients
{
    public interface IPremarketClient
    {
        Task<Result<string>> GetPremarketData(string symbol, StonkType type);
    }
}
