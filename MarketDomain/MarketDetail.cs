using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketDomain
{
    public class MarketDetail
    {
        public DateTime Date { get; set; }

        public float Open { get; set; }

        public float High { get; set; }

        public float Low { get; set; }

        public float Close { get; set; }
        public float AdjustedClose { get; set; }
        public float Volume { get; set; }
        public float DividendAmount { get; set; }
        public float SplitCoefficient { get; set; }
        public static IEnumerable<MarketDetail> Parse(Dictionary<DateTime, TimeDetail> data)
        {
            foreach (var item in data)
            {
                MarketDetail marketDetail = new();
                marketDetail.Date = item.Key;
                marketDetail.Open = item.Value.Open;
                marketDetail.High = item.Value.High;
                marketDetail.Low = item.Value.Low;
                marketDetail.Close = item.Value.Close;
                marketDetail.AdjustedClose = item.Value.AdjustedClose;
                marketDetail.Volume = item.Value.Volume;
                marketDetail.DividendAmount = item.Value.DividendAmount;
                marketDetail.SplitCoefficient = item.Value.SplitCoefficient;
                yield return marketDetail;
            }
        }
    }
}
