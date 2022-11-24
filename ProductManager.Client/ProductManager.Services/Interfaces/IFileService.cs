using ProductManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Services.Interfaces
{
    public interface IFileService
    {
        IEnumerable<Product> GetProductDataFromFile(string filePath);
    }
}
