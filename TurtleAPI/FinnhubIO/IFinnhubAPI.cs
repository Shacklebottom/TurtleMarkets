using MarketDomain;
using TurtleAPI.BaseClasses;

namespace TurtleAPI.FinnhubIO
{
    public interface IFinnhubAPI : ITurtleAPI
    {
        PreviousClose GetPreviousClose(string ticker);
        IEnumerable<RecommendedTrend> GetRecommendedTrend(string ticker);
    }
}
