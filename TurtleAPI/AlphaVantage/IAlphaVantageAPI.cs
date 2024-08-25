using MarketDomain.Enums;
using MarketDomain.Objects;

namespace TurtleAPI.AlphaVantage
{
    public interface IAlphaVantageAPI
    {
        Task<IEnumerable<MarketStatus>?> GetMarketStatus();
        Task<Dictionary<EnumPrestigeType, IEnumerable<Prominence>>?> GetPolarizedMarkets();
        Task<IEnumerable<ListedStatus>?> GetListedStatus(EnumListedStatusTypes listingType = EnumListedStatusTypes.Active);
    }
}
