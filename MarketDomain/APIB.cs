using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketDomain
{

    public class APIB : X
    {
        [JsonProperty("Source Data")]
        public MetaData? MetaData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonProperty("Daily Variations")]
        public Dictionary<DateTime, TimeDetail> TimeSeries { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
