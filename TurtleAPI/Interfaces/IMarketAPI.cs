using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketDomain;

namespace TurtleAPI.Interfaces
{
    public interface IMarketAPI
    {
        //IEnumerable<MarketDetail> ParseJson(string json);
        IEnumerable<MarketDetail> GetMarketDetails(string ticker, DateTime startDate, DateTime endDate);
    }
}
