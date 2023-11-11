using MarketDomain.Interfaces;

namespace TurtleSQL
{
    public interface ITickerRepository<T> : IRepository<T> where T : ITicker
    {
        IEnumerable<T> GetByTicker(string ticker);
    }
}
