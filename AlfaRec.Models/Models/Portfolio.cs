using System;
using System.Collections.Generic;
using System.Text;

namespace FundConnRec.Models.Models
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }

        public string ISIN { get; set; }

        public string Currency { get; set; }

        public decimal MarketValue { get; set; }

        public DateTime Date { get; set; }

        public List<Position> Positions { get; set; }
    }
}
