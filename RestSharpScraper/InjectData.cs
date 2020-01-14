using System;
using Microsoft.Data.SqlClient;

namespace RestSharpScraper
{
    public class InjectData
    {
        public static void InjectToDatabase(Crypto exchangedCrypto)
        {
            const string connection =
                @"Server=tcp:finance-scraper.database.windows.net,1433;Initial Catalog=CoinMarketCap;Persist Security Info=False;User ID=Dan;Password=iLOVEcareerdevs1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using SqlConnection dbConnection = new SqlConnection(connection);
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
}