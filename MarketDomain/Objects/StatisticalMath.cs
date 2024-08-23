namespace MarketDomain.Objects
{
    public class StatisticalMath
    {
        public double? Mean { get; set; } //average of all data
        public double? Median { get; set; } //middle of data
        public double? Mode { get; set; } //common occurance in data
        public double? Range { get; set; } //max value minus minimum value
        public double? QuartileDeviation { get; set; } //quarters meh'lad, quarters (half the difference between upper quartile (Q3) and the lower quartile (Q1)
        public double? Variance { get; set; } //average of the squared differences from the mean. How spread are the numbers from their average value?
        public double? StandardDeviation { get; set; } //square root of the variance. Measures how far the data deviates from the center on average.

    }
}
