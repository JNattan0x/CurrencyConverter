using Microsoft.AspNetCore.Mvc;
using App.Interfaces;

namespace App.Controllers
{
    public class ConverterController : BaseController
    {
        private IFileDataContext<Dictionary<string,string>> _currencyDataSetService;
        public ConverterController(IServiceProvider provider) : base(provider)
        {
            _currencyDataSetService =
                _provider.GetService(typeof(IFileDataContext<Dictionary<string,string>>))
                as IFileDataContext<Dictionary<string,string>>;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _currencyDataSetService.GetContextualData());
        }

        [HttpPost]
        public async Task<JsonResult> MakeConversion(
            string originCurrency, 
            string inputValue, 
            string finalCurrency)
        {   
            try
            {
                if(Double.Parse(inputValue) < 0)
                {
                    return new JsonResult(BadRequest());
                }
            } 
            catch(Exception ex) when(ex is ArgumentException || ex is FormatException)
            {
                return new JsonResult(BadRequest());
            }

            IFileDataContext<string> _xmlContextService = 
                _provider.GetService(typeof(IFileDataContext<string>))
                as IFileDataContext<string>;


            CurrencyModel model = new CurrencyModel();
            model.originCurrency = originCurrency;
            model.finalCurrency = finalCurrency;
            model.inputValue = inputValue;

            HttpResponseMessage response = new HttpResponseMessage();

            using(HttpClient client = new HttpClient())
            {   
                String apiKey = _xmlContextService.GetContextualData().Result;
                client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
                response = await client.GetAsync(
                    $"https://api.api-ninjas.com/v1/convertcurrency?have={originCurrency}&want={finalCurrency}&amount={inputValue}"
                );
            }
            return new JsonResult(Ok(response.Content.ReadAsStringAsync().Result));
        }
    }
}