using System;
using System.Collections.Generic;
using System.Text;

namespace Stonk.Application.Dtos.Stonks.Client
{
    public class StockInfoClientResponseDto
    {
        public string PageSource { get; set; }
        public string PageSourceUrl { get; set; }
        public string Symbol { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Sector { get; set; }
        public string SubSector { get; set; }
        public string PreviousPrice { get; set; }
        public string MarcketCap { get; set; }
        public string Income { get; set; }
        public string Sales { get; set; }
        public string Volume { get; set; }
        public string AverageVolume { get; set; }
        public string TargetPrice { get; set; }
        public string InsiderOwn { get; set; }
        public string InsiderTransition { get; set; }
        public string InstitutionOwn { get; set; }
        public string InstitutionTransition { get; set; }
        public string SharesOutstand { get; set; }
        public string SharesFloat { get; set; }
        public string Description { get; set; }
    }
}
