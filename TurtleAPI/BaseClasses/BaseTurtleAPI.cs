using BusinessLogic.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TurtleAPI.Exceptions;
using TurtleAPI.PolygonIO;

namespace TurtleAPI.BaseClasses
{
    public class BaseTurtleAPI : ITurtleAPI
    {
        protected readonly ILogger _logger;
        protected int _msToSleep;

        private BaseTurtleAPI()
        {
            _logger = new ConsoleLogger();
        }

        protected BaseTurtleAPI(ILogger? logger = null) : this(0, logger ?? new ConsoleLogger()) { }

        protected BaseTurtleAPI(int msToSleep, ILogger? logger = null)
        {
            _logger = logger ?? new ConsoleLogger();
            _msToSleep = msToSleep;
        }

        protected IEnumerable<T> CallAPI<T>(Uri uri, List<KeyValuePair<string, string>>? customHeaders = null, Func<string, IEnumerable<T>>? parser = null)
        {
            var client = new HttpClient
            {
                BaseAddress = uri
            };

            customHeaders?.ForEach(h => client.DefaultRequestHeaders.Add(h.Key, h.Value));

            var response = client.GetAsync(uri).Result;
            _logger.Log($"{response.StatusCode}");
            if (response.StatusCode != HttpStatusCode.OK) // 200 == OK
            {
                var message = $"Unexpected status code: {response.StatusCode}\nURI: {uri}\n{response.Content.ReadAsStringAsync().Result}";

                _logger.Log(message);
                throw new ApiException(message, response);
            }

            var responseString = response.Content.ReadAsStringAsync().Result;
            IEnumerable<T> baseData;
            if (parser == null)
            {
                baseData = new List<T> { JsonConvert.DeserializeObject<T>(responseString)  ??
                    throw new ParseException($"could not parse response as {typeof(T)}") };
            } else
            {
                baseData = parser.Invoke(responseString);
            }
            Thread.Sleep(_msToSleep);

            return baseData;
        }
    }
}
