using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketDomain.Interfaces;

namespace MarketDomain
{
    /*
 * "cash_amount": 0.22,
  "declaration_date": "2021-10-28", (intent to dividend payout  date)
  "dividend_type": "CD",
  "ex_dividend_date": "2021-11-05", (TradeBeforeDividend)
  "frequency": 4,
  "pay_date": "2021-11-11", (actual payout date)
  "record_date": "2021-11-08", (actual own before date)
  "ticker": "AAPL"
 */
    public class DividendDetails : ITicker
    {
        public override string ToString()
        {
            return $"Ticker: {Ticker}\tPayoutPerShare: {PayoutPerShare}\tDividendType: {DividendType}\tPayoutFrequency: {PayoutFrequency}\t OwnBeforeDate: {OwnBeforeDate}";
        }
        public string Ticker { get; set; } = string.Empty;
        public double? PayoutPerShare { get; set; }
        public string? DividendType { get; set; }
        public int? PayoutFrequency { get; set; }
        //Company announce date of intent to payout a dividend.
        public DateTime? DividendDeclaration { get; set; }
        //Date when it first trades without the dividend.
        public DateTime? OpenBeforeDividend { get; set; }
        //The date when the dividend pays out.
        public DateTime? PayoutDate { get; set; }
        //Required before date to qualify for dividend payout.
        public DateTime? OwnBeforeDate { get; set; }
        public int Id { get; set; }
    }
}
