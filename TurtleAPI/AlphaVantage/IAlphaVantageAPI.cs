using MarketDomain;
using MarketDomain.Enums;
using TurtleAPI.BaseClasses;

namespace TurtleAPI.AlphaVantage
{
    public interface IAlphaVantageAPI : ITurtleAPI
    {
        IEnumerable<MarketStatus>? GetMarketStatus();
        Dictionary<PrestigeType, IEnumerable<Prominence?>?> GetPolarizedMarkets();
        IEnumerable<ListedStatus> GetListedStatus(ListedStatusTypes listingType = ListedStatusTypes.Active);
    }
}
