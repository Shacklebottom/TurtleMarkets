using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleAPI.PolygonIO.Responses
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    internal class PolygonDividendResponse
    {
        public PolygonDividendResults[] results { get; set; } = Array.Empty<PolygonDividendResults>();
    }
}
