using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleAPI.AlphaVantage
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class AlphaVProminenceResponse
    {
        public AlphaVProminenceResult[]? top_gainers { get; set; }
        public AlphaVProminenceResult[]? top_losers { get; set; }
        public AlphaVProminenceResult[]? most_actively_traded { get; set; }
    }
}
