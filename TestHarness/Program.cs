using MarketDomain;
using TurtleAPI;
using TurtleAPI.PolygonIO;
using TurtleAPI.AlphaVantage;
using TurtleAPI.Interfaces;

IMarketAPI poly = new PolygonAPI();
IMarketAPI alpha = new AlphaVantageAPI();
var marketDetails = new List<IEnumerable<MarketDetail>>();

var ticker = "MSFT";
var startDate = DateTime.Now;
var endDate = DateTime.Now.AddDays(-7);

marketDetails.Add(poly.GetMarketDetails(ticker, startDate, endDate));
marketDetails.Add(alpha.GetMarketDetails(ticker, startDate, endDate));
/****************************************/
//var json = File.ReadAllText("C:\\Code\\c#\\TurtleMarkets\\testdata.json");
//var bob = SourceHeader.ParseJson(json);

//Console.WriteLine(bob.MetaData.Symbol);

//foreach (var item in bob.MarketDetails)
//{
//    Console.WriteLine($"{item.Date} : {item.Open}");
//}

/****************************************/
//using System.Data.SqlClient;

//var connection = new SqlConnection();
//connection.ConnectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MarketData;Data Source=.";


//var command = connection.CreateCommand();
//command.CommandText = "SELECT Ticker FROM Transactions";


//connection.Open();
//var reader = command.ExecuteReader();
////while (reader.Read())
////{
////    Console.WriteLine(reader.GetString(0));
////}
//foreach (var row in Record.Read(reader))
//{
//    Console.WriteLine(row.Ticker);
//}
//connection.Close();


//public class Record
//{
//    public string? Ticker { get; set; }
//    public static IEnumerable<Record> Read(SqlDataReader reader)
//    {
//        while (reader.Read())
//        {
//            yield return new Record { Ticker = reader.GetString(0) };
//        }
//    }
//}