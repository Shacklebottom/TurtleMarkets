using MarketDomain.Interfaces;


namespace MarketDomain.Objects
{
    public class MarketServiceLocator : IServiceLocator
    {
        private readonly Dictionary<string, object> _services = [];

        public T GetService<T>(string key)
        {
            return (T)_services[key];
        }

        public void RegisterService<T>(string key, Func<T> service)
        {
            _services[key] = service() ?? throw new NullReferenceException();
        }
    }
}
