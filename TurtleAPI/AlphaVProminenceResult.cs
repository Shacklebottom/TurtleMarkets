using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleAPI
{
    public class AlphaVProminenceResult
    {
        public string? ticker { get; set; }
        public decimal? price { get; set; }
        public decimal? change_amount { get; set; }
        public string? change_percentage { get; set; }
        public long? volume { get; set; }
    }
}
