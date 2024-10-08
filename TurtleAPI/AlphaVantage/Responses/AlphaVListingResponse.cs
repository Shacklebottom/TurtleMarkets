﻿using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.AlphaVantage.Responses
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class AlphaVListingResponse
    {
        // CSV header looks like this: symbol,name,exchange,assetType,ipoDate,delistingDate,status
        public string symbol { get; set; } = string.Empty;
        public string? name { get; set; }
        public string? exchange { get; set; }
        public string? assetType { get; set; }
        public DateTime? ipoDate { get; set; }
        public DateTime? delistingDate { get; set; }
        public string? status { get; set; }
    }
}
