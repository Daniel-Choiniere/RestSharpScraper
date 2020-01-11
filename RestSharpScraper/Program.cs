using System;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace RestSharpScraper
{
    class Program
    {
        static void Main()
        {
            var apiCallTask = ApiHelper.ApiCall("WcATy9IyxwbTeHbB0lsYcTqr6bAeQEmV");
            var result = apiCallTask.Result;
            Console.WriteLine(result);
        }
    }

    class ApiHelper
    {
        public static async Task<string> ApiCall(string apiKey)
        {
            RestClient client = new RestClient("https://www.alphavantage.co");
            RestRequest request =
                new RestRequest(
                    $"/query?function=DIGITAL_CURRENCY_MONTHLY&symbol=BTC&market=USD&apikey={apiKey}",
                    Method.GET);


            var response = await client.ExecuteTaskAsync(request);
            return response.Content;
        }
    }
}