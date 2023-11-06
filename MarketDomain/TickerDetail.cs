using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MarketDomain
{
    public class TickerDetail : IEntity
    {
        public override string ToString()
        {
            return $"Ticker: {Ticker}\tListDate: {ListDate}\tName: {Name}\tDescription: {Description}";
        }
        public string? Ticker { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public TickerAddress? Address { get; set; }
        public long? TotalEmployees { get; set; }
        public DateOnly? ListDate { get; set; }
        public int Id { get; set; }
    }
}
