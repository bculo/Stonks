using System;
using System.Collections.Generic;
using System.Text;

namespace Stonk.Application.Dtos.Premarket
{
    public class PremarketDataResponseDto
    {
        public string Symbol { get; set; }
        public string Price { get; set; }
        public string Market { get; set; }
    }
}
