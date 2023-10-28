using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketObjects
{
    public class MarketEnum
    {
        public enum TradeType
        {
            Stock,
            Bond,
            Commodity,
            Currency,
            Option,
            Future,
            ETF,
            Crypto,
            Other
        }
    }
}
