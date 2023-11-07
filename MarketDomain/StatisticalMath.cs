using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketDomain
{
    public class StatisticalMath
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Mean { get; set; } //average of all data
        public decimal Median { get; set; } //middle of data
        public decimal Mode { get; set; } //common occurance in data
        public decimal Range { get; set; } //max value minus minimum value
        public decimal QuartileDeviation { get; set; } //quarters meh'lad, quarters (half the difference between upper quartile (Q3) and the lower quartile (Q1)
        public decimal Variance { get; set; } //average of the squared differences from the mean. How spread are the numbers from their average value?
        public decimal StandardDeviation { get; set; } //square root of the variance. Measures how far the data deviates from the center on average.

    }
}
