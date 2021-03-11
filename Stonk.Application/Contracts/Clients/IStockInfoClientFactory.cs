using Stonk.Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stonk.Application.Contracts.Clients
{
    public interface IStockInfoClientFactory
    {
        List<IStonkInfoClient> GetAllClients();
        IStonkInfoClient GetClient(StockInfoSource source);
    }
}
