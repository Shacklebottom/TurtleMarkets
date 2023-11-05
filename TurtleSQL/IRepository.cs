using MarketDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleSQL
{
    public interface IRepository<T> where T : IEntity
    {
        T? GetById(int id);
        IEnumerable<T>? GetAll();
        int Save(T entity);
    }
}
