using MarketDomain;

namespace TurtleAPI.FinnhubIO
{
    public interface IFinnhubAPI
    {
        Task<PreviousClose?> GetPreviousClose(string ticker);
        Task<IEnumerable<RecommendedTrend>?> GetRecommendedTrend(string ticker);
    }
}
