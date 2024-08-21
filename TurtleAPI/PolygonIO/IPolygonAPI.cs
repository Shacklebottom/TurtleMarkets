using MarketDomain;

namespace TurtleAPI.PolygonIO
{
    public interface IPolygonAPI
    {
        Task<IEnumerable<DividendDetails>?> GetDividendDetails(string ticker);
        Task<TickerDetail?> GetTickerDetails(string ticker);
        Task<IEnumerable<MarketHoliday>?> GetMarketHoliday();
    }
}
