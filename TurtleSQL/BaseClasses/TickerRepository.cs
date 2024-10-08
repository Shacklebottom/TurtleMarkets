﻿using MarketDomain.Interfaces;
using TurtleSQL.Interfaces;

namespace TurtleSQL.BaseClasses
{
    public class TickerRepository<T> : Repository<T>, ITickerRepository<T> where T : ITicker
    {
        public IEnumerable<T> GetByTicker(string ticker)
        {
            var cmd = _sqlConnection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM {TableName} WHERE Ticker = '{ticker}'";

            _sqlConnection.Open();
            var rdr = cmd.ExecuteReader();
            IEnumerable<T> result = AllFromReader(rdr).ToList();
            _sqlConnection.Close();

            return result;
        }
    }
}
