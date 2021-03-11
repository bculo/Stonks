using Microsoft.Extensions.DependencyInjection;
using Stonk.Application.Contracts.Clients;
using Stonk.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stonk.Service.Clients
{
    public class StockInfoClientFactory : IStockInfoClientFactory
    {
        private readonly IServiceProvider _provider;

        public StockInfoClientFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public List<IStonkInfoClient> GetAllClients()
        {
            return _provider.GetServices<IStonkInfoClient>().ToList();
        }

        public IStonkInfoClient GetClient(StockInfoSource source)
        {
            switch (source)
            {
                case StockInfoSource.FinViz:
                    return _provider.GetRequiredService<FinVizStockClient>();
            }

            return null;
        }
    }
}
