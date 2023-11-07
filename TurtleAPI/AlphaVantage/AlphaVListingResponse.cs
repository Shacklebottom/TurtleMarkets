using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleAPI.AlphaVantage
{
    public class AlphaVListingResponse
    {
        public string? symbol { get; set; }
        public string? name { get; set; }
        public string? exchange { get; set; }
        public string? assetType { get; set; }
        public DateOnly? ipoDate { get; set; }
        public DateOnly? delistingDate { get; set; }
        public string? status { get; set; }
    }
}
