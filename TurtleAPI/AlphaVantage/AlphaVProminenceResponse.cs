using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleAPI.AlphaVantage
{
    public class AlphaVProminenceResponse
    {
        public AlphaVProminenceResult[]? top_gainers { get; set; }
        public AlphaVProminenceResult[]? top_losers { get; set; }
        public AlphaVProminenceResult[]? most_actively_traded { get; set; }
    }
}
