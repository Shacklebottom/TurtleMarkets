using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketDomain
{
    public class SourceHeader
    {
        public string? Source { get; set; }
        public MetaData? MetaData { get; set; }
        public IEnumerable<MarketDetail>? MarketDetails { get; set; }
    }
}
