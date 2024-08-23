
namespace MarketDomain.Interfaces
{
    public interface IServiceLocator
    {
        T GetService<T>(string key);

        void RegisterService<T>(string key, Func<T> service);
    }
}
