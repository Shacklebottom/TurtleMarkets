using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketDomain
{
    public class MarketDetail
    {
        public DateTime? Date { get; set; }

        public float? Open { get; set; }

        public float? High { get; set; }

        public float? Low { get; set; }

        public float? Close { get; set; }
        public float? AdjustedClose { get; set; }
        public float? Volume { get; set; }
        public float? VolumeWeighted { get; set; }
        public float? DividendAmount { get; set; }
        public float? SplitCoefficient { get; set; }

    }
}
