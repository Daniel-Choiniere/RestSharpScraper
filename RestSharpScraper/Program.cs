using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Data.SqlClient;

namespace RestSharpScraper
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine(
                "What crypto currency would you like to find the exchange rate for? Please use the currency's abbreviation");
            var cryptoToExchange = Console.ReadLine();
            var currencyFrom = cryptoToExchange;

            Console.WriteLine("What currency would you like to convert to? Please use the currency's abbreviation");
            var exchangeTo = Console.ReadLine();
            var currencyTo = exchangeTo;

            var apiKey = "WcATy9IyxwbTeHbB0lsYcTqr6bAeQEmV";

            var apiCallTask = ApiHelper.ApiCall(currencyFrom, currencyTo, apiKey);
            var result = apiCallTask.Result;

            Crypto exchangedCrypto = GetData.ConsumeAndInitiate(result);
            
            InjectData.InjectToDatabase(exchangedCrypto);
        }
    }
}