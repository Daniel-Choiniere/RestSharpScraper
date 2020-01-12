using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Data.SqlClient;

namespace RestSharpScraper
{
    class Program
    {
        static void Main()
        {
            var currenceyFrom = "XRP";
            var currencyTo = "USD";
            var apiKey = "WcATy9IyxwbTeHbB0lsYcTqr6bAeQEmV";
            var apiCallTask = ApiHelper.ApiCall(currenceyFrom, currencyTo, apiKey);
            var result = apiCallTask.Result;

            Crypto exchangedCrypto = ConsumeandIntitate(result);

            InjectToDatabase(exchangedCrypto);

        }

        private static void InjectToDatabase(Crypto exchangedCrypto)
        {
             string connection =
                @"Server=tcp:finance-scraper.database.windows.net,1433;Initial Catalog=CoinMarketCap;Persist Security Info=False;User ID=Dan;Password=iLOVEcareerdevs1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (SqlConnection dbConnection = new SqlConnection(connection))
            {
                dbConnection.Open();
                
                        SqlCommand insertCommand = new SqlCommand(
                            "INSERT into dbo.ExchangeRates (DateTimePulled, FromCode, FromName, ToCode, ToName, ExchangeRate, LastRefreshed, TimeZone, BidPrice, AskPrice) VALUES (@DateTime, @fromCode, @fromName, @toCode, @toName, @exchangeRate, @lastRefreshed, @timeZone, @bidPrice, @askPrice)",
                            dbConnection);
                        insertCommand.Parameters.AddWithValue("@dateTime", DateTime.Now);
                        insertCommand.Parameters.AddWithValue("@fromCode", exchangedCrypto.FromCode);
                        insertCommand.Parameters.AddWithValue("@fromName", exchangedCrypto.FromName);
                        insertCommand.Parameters.AddWithValue("@toCode", exchangedCrypto.ToCode);
                        insertCommand.Parameters.AddWithValue("@toName", exchangedCrypto.ToName);
                        insertCommand.Parameters.AddWithValue("@exchangeRate", exchangedCrypto.ExchangeRate);
                        insertCommand.Parameters.AddWithValue("@lastRefreshed", exchangedCrypto.LastRefreshed);
                        insertCommand.Parameters.AddWithValue("@timeZone", exchangedCrypto.TimeZone);
                        insertCommand.Parameters.AddWithValue("@bidPrice", exchangedCrypto.BidPrice);
                        insertCommand.Parameters.AddWithValue("@askPrice", exchangedCrypto.AskPrice);
                        
                        insertCommand.ExecuteNonQuery();
                        
                        Console.WriteLine("Data Collection Successful");
                        dbConnection.Close();
            }
        }
        

        private static Crypto ConsumeandIntitate(string result)
        {
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
            
            Crypto exchangedCrypto = new Crypto
            {
                FromCode = jsonResponse["Realtime Currency Exchange Rate"]["1. From_Currency Code"].ToString(),
                FromName = jsonResponse["Realtime Currency Exchange Rate"]["2. From_Currency Name"].ToString(),
                ToCode = jsonResponse["Realtime Currency Exchange Rate"]["3. To_Currency Code"].ToString(),
                ToName = jsonResponse["Realtime Currency Exchange Rate"]["4. To_Currency Name"].ToString(),
                ExchangeRate = jsonResponse["Realtime Currency Exchange Rate"]["5. Exchange Rate"].ToString(),
                LastRefreshed = jsonResponse["Realtime Currency Exchange Rate"][ "6. Last Refreshed"].ToString(),
                TimeZone = jsonResponse["Realtime Currency Exchange Rate"][  "7. Time Zone"].ToString(),
                BidPrice = jsonResponse["Realtime Currency Exchange Rate"][  "8. Bid Price"].ToString(),
                AskPrice= jsonResponse["Realtime Currency Exchange Rate"]["9. Ask Price"].ToString(),
            };
            
            return exchangedCrypto;
        }
    }

    class ApiHelper
    {
        public static async Task<string> ApiCall(string currenceyFrom, string currencyTo, string apiKey)
        {
            RestClient client = new RestClient("https://www.alphavantage.co");
            RestRequest request =
                new RestRequest(
                    $"/query?function=CURRENCY_EXCHANGE_RATE&from_currency={currenceyFrom}&to_currency={currencyTo}&apikey=demo{apiKey}",
                    Method.GET);
            
            var response = await client.ExecuteTaskAsync(request);
            return response.Content;
        }
    }
}