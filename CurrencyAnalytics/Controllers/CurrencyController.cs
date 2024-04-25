using CurrencyAnalytics.Models;
using CurrencyAnalytics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace CurrencyAnalytics.Controllers
{
    [Route("currencies")]
    [ApiController]
    public class CurrencyController:ControllerBase
    {
        private readonly CurrencyService _currencyService;
        public CurrencyController(CurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAverage(string startDate, string endDate)
        {

            try
            {
                List<CurrencyModel> currencies = _currencyService.GetAll(startDate, endDate);

                if (currencies != null)
                {
                    return Ok(currencies);
                }
                else
                {
                    return StatusCode(500, "Failed to retrieve currencies");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("average")]
        public IActionResult GetAverages(string startDate, string endDate)
        {

            try
            {
                
                List<CurrencyModel> currencies = _currencyService.GetAll(startDate, endDate);

                if (currencies != null)
                {
                    var calculator = new CurrencyAverageCalculatorService();
                    var averages = calculator.CalculateAverages(currencies);
                        return Ok(averages);
                }
                else
                {
                    return StatusCode(500, "Failed to retrieve currencies");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
