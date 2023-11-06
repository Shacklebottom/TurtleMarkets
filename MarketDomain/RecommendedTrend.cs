﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MarketDomain
{
    public class RecommendedTrend : IEntity
    {
        public override string ToString()
        {
            return $"Ticker: {Ticker}\tPeriod: {Period}\tBuy: {Buy}\tHold: {Hold}\tSell {Sell}";
        }
        public string? Ticker { get; set; }
        public int? Buy { get; set; }
        public int? Hold { get; set; }
        public DateOnly? Period { get; set; }
        public int? Sell { get; set; }
        public int? StrongBuy { get; set; }
        public int? StrongSell { get; set; }
        public int Id { get; set; }
    }
}
