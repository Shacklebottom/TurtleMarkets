using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketDomain.Interfaces
{
    public interface ITicker : IEntity
    {
        string Ticker { get; set; }
    }
}
