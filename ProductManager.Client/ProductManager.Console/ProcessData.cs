using ProductManager.Services;
using ProductManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager
{
    public class ProcessData
    {
        private readonly IWebService _webService;
        private readonly IFileService _fileService;
        public ProcessData(IWebService webService, IFileService fileService) 
        {
            _webService = webService;
            _fileService = fileService;
        }

        public async void Run()
        {
            Console.WriteLine("******************Product Manager Running***************");
            Console.WriteLine("********************************************************");

            string filePath = string.Empty;

            do
            {
                filePath = Console.ReadLine();
                if (!string.IsNullOrEmpty(filePath) && !string.IsNullOrWhiteSpace(filePath))
                {
                    var products = _fileService.GetProductDataFromFile(filePath);
                    await _webService.SubmitProducts(products);
                }

            } while (!string.IsNullOrEmpty(filePath) && !string.IsNullOrWhiteSpace(filePath));
        }
    }
}
