using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProductManager.Models;
using ProductManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Services
{
    public class WebService : IWebService
    {
        private readonly AppSettings _appSettings;
        public WebService(AppSettings appSettings) {
            _appSettings = appSettings;
        }

        public async Task SubmitProducts(IEnumerable<Product> products)
        {
            using (HttpClient http = new HttpClient())
            {
                http.BaseAddress = new Uri(_appSettings.ApplicationBaseURL);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(products), Encoding.UTF8, "application/json");

                try
                {
                    var responseTask = http.PostAsync("api/Product/Bulk", content);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                    }
                    if (!result.IsSuccessStatusCode)
                    {
                        throw new Exception(result.ReasonPhrase + JsonConvert.SerializeObject(result));
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
