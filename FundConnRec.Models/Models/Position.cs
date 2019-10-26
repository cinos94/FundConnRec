using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundConnRec.Models.Models
{
    public class Position
    {
        public int PositionId { get; set; }

        public decimal MarketValue { get; set; }

        public int SecurityId { get; set; }

        public int PortfolioId { get; set; }

        [JsonIgnore]
        public virtual Portfolio Portfolio { get; set; }

        public virtual Security Security { get; set; }
    }
}
