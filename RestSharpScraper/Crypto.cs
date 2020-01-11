namespace RestSharpScraper
{
    public class Crypto
    {
        public string FromCode { get; set; }
        public string FromName { get; set; }
        public string ToCode { get; set; }
        public string ToName { get; set; }
        public string ExchangeRate { get; set; }
        public string LastRefreshed { get; set; }
        public string TimeZone { get; set; }
        public string BidPrice { get; set; }
        public string AskPrice { get; set; }
    }
}