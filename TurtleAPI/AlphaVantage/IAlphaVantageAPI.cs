using MarketDomain.Enums;
using MarketDomain.Objects;

namespace TurtleAPI.AlphaVantage
{
    public interface IAlphaVantageAPI
    {
        Task<IEnumerable<MarketStatus>?> GetMarketStatus();
        Task<Dictionary<PrestigeType, IEnumerable<Prominence>>?> GetPolarizedMarkets();
        Task<IEnumerable<ListedStatus>?> GetListedStatus(ListedStatusTypes listingType = ListedStatusTypes.Active);
    }
}
