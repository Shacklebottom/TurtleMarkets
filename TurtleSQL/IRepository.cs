using MarketDomain.Interfaces;

namespace TurtleSQL
{
    public interface IRepository<T> where T : IEntity
    {
        T? GetById(int id);
        IEnumerable<T>? GetAll();
        int Save(T entity);

    }
}
