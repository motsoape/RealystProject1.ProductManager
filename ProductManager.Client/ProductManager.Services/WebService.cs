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
        public WebService() { }

        public async Task SubmitProducts(IEnumerable<Product> products)
        {
            using (HttpClient http = new HttpClient())
            {
                http.BaseAddress = new Uri("http://localhost:5089/");
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(products), Encoding.UTF8, "application/json");

                try
                {
                    var responseMessage = await http.PostAsync("api/Product/Bulk", content);
                    var response = await responseMessage.Content.ReadAsStringAsync();

                    if (!responseMessage.IsSuccessStatusCode)
                    {
                        throw new Exception(responseMessage.ReasonPhrase + JsonConvert.SerializeObject(responseMessage));
                    }
                    if (response.ToString().Contains("ERROR"))
                    {
                        throw new Exception(response.ToString() + JsonConvert.SerializeObject(responseMessage));
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
