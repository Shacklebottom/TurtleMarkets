using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TurtleAPI.Exceptions
{
	[Serializable]
	public class ApiException : Exception
	{
		public HttpResponseMessage? Response { get; private set; } = null;

		public ApiException() { }
		public ApiException(HttpResponseMessage response) { Response = response; }
		public ApiException(string message) : base(message) { }
		public ApiException(string message, HttpResponseMessage response) : base(message) { Response = response; }
		public ApiException(string message, Exception inner) : base(message, inner) { }
		public ApiException(string message, Exception inner, HttpResponseMessage response) : base(message, inner) { Response = response; }
		protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
