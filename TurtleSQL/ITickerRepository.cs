using MarketDomain;

namespace TurtleSQL
{
    public interface ITickerRepository<T> : IRepository<T> where T : ITicker
    {
        IEnumerable<T> GetByTicker(string ticker);
    }
}
