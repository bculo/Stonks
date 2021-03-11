using System;
using System.Collections.Generic;
using System.Text;

namespace Stonk.Application.Dtos.Stonks
{
    public class PremarketDataResponseDto
    {
        public string Symbol { get; set; }
        public string Price { get; set; }
        public string Market { get; set; }
        public string PriceChange { get; set; }
        public string PriceChangePercantage { get; set; }
        public string PreviuosClose { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateTimeStamp { get; set; }
    }
}
