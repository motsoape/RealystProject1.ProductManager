using ProductManager.Models;
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
            Console.WriteLine("******************Product Manager Client Running***************");
            Console.WriteLine("***************************************************************");

            string filePath = string.Empty;

            Console.Write("Enter file path or press enter to exit: ");
            do
            {
                var errors = false;
                filePath = Console.ReadLine();
                if (!string.IsNullOrEmpty(filePath) && !string.IsNullOrWhiteSpace(filePath))
                {
                    IEnumerable<Product> products = new List<Product>();
                    try
                    {
                        products = _fileService.GetProductDataFromFile(filePath);
                    }
                    catch(Exception ex)
                    {
                        errors = true;
                        Console.WriteLine("Failed to open the file. Please make sure the file is accessible.");
                    }
                    try
                    {
                        await _webService.SubmitProducts(products);
                    }catch(Exception ex)
                    {
                        errors = true;
                        Console.WriteLine("Failed to submit data.");
                    }

                    if (!errors)
                    {
                        Console.WriteLine("File processed successfully.");
                    }
                    Console.Write("Enter file path or press enter to exit: ");
                }
                
            } while (!string.IsNullOrEmpty(filePath) && !string.IsNullOrWhiteSpace(filePath));
        }
    }
}
