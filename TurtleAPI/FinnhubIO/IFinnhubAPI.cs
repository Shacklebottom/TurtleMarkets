using MarketDomain;

namespace TurtleAPI.FinnhubIO
{
    public interface IFinnhubAPI 
    {
    PreviousClose GetPreviousClose(string ticker);
    }
}
