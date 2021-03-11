using System;
using System.Collections.Generic;
using System.Text;

namespace Stonk.Application.Dtos.Stonks
{
    public class StockInfoRequestDto
    {
        public string Symbol { get; set; }
        public int Source { get; set; }
    }
}
