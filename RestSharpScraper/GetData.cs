using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace RestSharpScraper
{
    public class GetData
    {
        public static Crypto ConsumeAndInitiate(string result)
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

    internal static class ApiHelper
    {
        public static async Task<string> ApiCall(string currencyFrom, string currencyTo, string apiKey)
        {
            RestClient client = new RestClient("https://www.alphavantage.co");
            RestRequest request =
                new RestRequest(
                    $"/query?function=CURRENCY_EXCHANGE_RATE&from_currency={currencyFrom}&to_currency={currencyTo}&apikey=demo{apiKey}",
                    Method.GET);
            
            var response = await client.ExecuteTaskAsync(request);
            return response.Content;
        }
    }
}