using AutoMapper;
using Microsoft.Extensions.Logging;
using Stonk.Application.Contracts.Clients;
using Stonk.Application.Contracts.Modules;
using Stonk.Application.Dtos.Stonks;
using Stonk.Application.Dtos.Stonks.Client;
using Stonk.Application.Extensions;
using Stonk.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stonk.Application.Modules
{
    public class StonkInformationService : IStonkInformationService
    {
        private readonly ILogger<StonkInformationService> _logger;
        private readonly IStockInfoClientFactory _factory;
        private readonly IMapper _mapper;

        public StonkInformationService(ILogger<StonkInformationService> logger,
            IStockInfoClientFactory factory,
            IMapper mapper)
        {
            _logger = logger;
            _factory = factory;
            _mapper = mapper;
        }

        public Task<Result<StockInfoResponseDto>> GetStockInfo(StockInfoRequestDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultArray<StockInfoResponseDto>> GetStockInfos(string symbol)
        {
            var clients = _factory.GetAllClients();

            if (!clients.Any())
            {
                return ResultExtensions.FailureArray<StockInfoResponseDto>("Unexpected exception");
            }

            var clientTasks = await LaunchClientsCall(clients, symbol);

            var finishedTasks = clientTasks.Cast<Task<Result<StockInfoClientResponseDto>>>();

            var results = new List<StockInfoResponseDto>();

            foreach(var finshedTask in finishedTasks)
            {
                var instance = HandleClientResponse(finshedTask.Result);

                if (instance == null)
                {
                    var dto = _mapper.Map<StockInfoResponseDto>(instance);
                    results.Add(dto);
                }
            }

            return ResultExtensions.Success(instances: results);
        }

        private async Task<List<Task>> LaunchClientsCall(List<IStonkInfoClient> clients, string symbol)
        {
            var clientTasks = new List<Task>();

            foreach (var client in clients)
            {
                clientTasks.Add(client.GetStockInfo(symbol));
            }

            await Task.WhenAll(clientTasks);

            return clientTasks;
        }

        private StockInfoClientResponseDto HandleClientResponse(Result<StockInfoClientResponseDto> response)
        {
            if (response.Succedded)
            {
                return response.Instance;
            }

            return null;
        }
    }
}
