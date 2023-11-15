using MarketDomain;
using TurtleAPI.BaseClasses;

namespace TurtleAPI.PolygonIO
{
    public interface IPolygonAPI : ITurtleAPI
    {
        IEnumerable<DividendDetails> GetDividendDetails(string ticker);
        TickerDetail GetTickerDetails(string ticker);
    }
}
