using CurrencyAnalytics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;

namespace CurrencyAnalytics.Services
{
        public class CurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CurrencyService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient("HnbApiClient");
        }

        public List<CurrencyModel> GetAll(string startDate, string endDate)
        {
            string apiUrl = _configuration.GetSection("HnbApi:Url").Value;
            string fullUrl = $"{apiUrl}?datum-primjene-od={startDate}&datum-primjene-do={endDate}";


            try
            {
                HttpResponseMessage response = _httpClient.GetAsync(fullUrl).Result;

                if (response.IsSuccessStatusCode)
                {                   
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    List<CurrencyModel> currencies = JsonSerializer.Deserialize<List<CurrencyModel>>(responseBody);

                    return currencies;
                  
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving currencies: {ex.Message}");
            }
        }

    }
}
