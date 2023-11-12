using MarketDomain.Interfaces;

namespace TurtleSQL.Interfaces
{
    public interface ITickerRepository<T> : IRepository<T> where T : ITicker
    {
        IEnumerable<T> GetByTicker(string ticker);
    }
}
