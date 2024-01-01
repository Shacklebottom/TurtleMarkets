using MarketDomain.Interfaces;

namespace TurtleSQL.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        T? GetById(int id);
        IEnumerable<T> GetAll();
        int Save(T entity);
        void TruncateTable();
    }
}
