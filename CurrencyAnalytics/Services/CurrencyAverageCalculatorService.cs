using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyAnalytics.Models;


public class CurrencyAverageCalculatorService
{
    public List<CurrencyAverageModel> CalculateAverages(List<CurrencyModel> currencies)
    {
        if (currencies == null || currencies.Count == 0)
        {
            throw new ArgumentException("List of currencies is null or empty.");
        }

        var averagesByCurrency = currencies
            .GroupBy(c => c.sifra_valute)
            .Select(g => new CurrencyAverageModel
            {
                CurrencyCode = g.Key,
                CurrencyName = g.First().valuta,
                BuyRateAverage = g.Average(c => Convert.ToDouble(c.kupovni_tecaj)),
                MiddleRateAverage = g.Average(c => Convert.ToDouble(c.srednji_tecaj)),
                SellRateAverage = g.Average(c => Convert.ToDouble(c.prodajni_tecaj))
            })
            .ToList();

        return averagesByCurrency;
    }
}
