using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundConnRec.Models.Models
{
    public class Security
    {
        public int SecurityId { get; set; }

        public string ISIN { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public string Country { get; set; }

        [JsonIgnore]
        public ICollection<Position> Positions { get; set; }
    }
}
