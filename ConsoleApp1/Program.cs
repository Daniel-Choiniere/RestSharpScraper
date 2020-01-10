using System;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace ConsoleApp1
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
            RestClient client = new RestClient("https://blockchain.info/ticker");
            RestRequest request = new RestRequest($"home.json?api-key={apiKey}", Method.GET);
            var response = await client.ExecuteTaskAsync(request);
            return response.Content;
        }
    }
}