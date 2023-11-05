using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketDomain
{
    public class PreviousClose : IEntity
    {
        public override string ToString()
        {
            return $"Ticker: {Ticker}\tDate: {Date}\tOpen: {Open}\tClose: {Close}";
        }
        public string? Ticker { get; set; }
        public DateTime? Date { get; set; }

        public float? Open { get; set; }

        public float? Close { get; set; }
        public float? High { get; set; }

        public float? Low { get; set; }

        public float? Volume { get; set; }


    }
}
