﻿

namespace MarketDomain
{
    public class MarketStatus : IEntity
    {
        //this will probably be used as a variable for logic gates, and not placed in a repo
        public override string ToString()
        {
            return $"Market Type: {MarketType}\tRegion: {Region}\tExchange: {Exchange}\tLocal Open: {LocalOpen}\tLocal Close: {LocalClose}\tStatus: {Status}";
        }
        public string? MarketType { get; set; }
        public string? Region { get; set; }
        public string? Exchange { get; set; }
        public string? LocalOpen { get; set; }
        public string? LocalClose { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public int Id { get; set; }
    }
}
