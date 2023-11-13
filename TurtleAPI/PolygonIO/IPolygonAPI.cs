using MarketDomain;

namespace TurtleAPI.PolygonIO
{
    public interface IPolygonAPI
    {
        IEnumerable<DividendDetails> GetDividendDetails(string ticker);
    }
}
