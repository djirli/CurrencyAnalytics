namespace CurrencyAnalytics.Models
{
    public class CurrencyAverageModel
    {
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public double BuyRateAverage { get; set; }
        public double MiddleRateAverage { get; set; }
        public double SellRateAverage { get; set; }
    }
}
