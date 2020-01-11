using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestSharpScraper
{
    class Program
    {
        static void Main()
        {
            var currenceyFrom = "BTC";
            var currencyTo = "USD";
            var apiKey = "WcATy9IyxwbTeHbB0lsYcTqr6bAeQEmV";
            var apiCallTask = ApiHelper.ApiCall(currenceyFrom, currencyTo, apiKey);
            var result = apiCallTask.Result;

            InjectandIntitate(result);
        }

        private static void InjectandIntitate(string result)
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
            
            Console.WriteLine(exchangedCrypto.FromCode);
            Console.WriteLine(exchangedCrypto.ExchangeRate);

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