using AutoMapper;
using Stonk.Application.Dtos.Stonks;
using Stonk.Application.Dtos.Stonks.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stonk.Application.Modules.Mappings
{
    public class StockProfile : Profile
    {
        public StockProfile()
        {
            CreateMap<StockInfoClientResponseDto, StockInfoResponseDto>();                
        }
    }
}
