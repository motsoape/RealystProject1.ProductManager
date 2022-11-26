using ProductManager.Models;
using ProductManager.Services.Interfaces;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;

namespace ProductManager.Services
{
    public class FileService : IFileService
    {
        public FileService() { }

        public IEnumerable<Product> GetProductDataFromFile(string filePath)
        {
            try
            {
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;

                var products = new List<Product>();

                for (int i = 1; i <= rowCount; i++)
                {
                    var product = new Product();
                    if (colCount > 0 && xlRange.Cells[i, 1] != null && xlRange.Cells[i, 1].Value2 != null)
                    {
                        product.Name = xlRange.Cells[i, 1].Value2.ToString();
                    }
                    if (colCount > 1 && xlRange.Cells[i, 2] != null && xlRange.Cells[i, 2].Value2 != null)
                    {
                        product.Price = (double)(xlRange.Cells[i, 2].Value2);
                    }
                    if (colCount > 2 && xlRange.Cells[i, 3] != null && xlRange.Cells[i, 3].Value2 != null)
                    {
                        product.ReleaseDate = DateTime.Parse(xlRange.Cells[i, 3].Value2);
                    }
                    products.Add(product);
                }

                return products;
            }catch(Exception ex)
            {
                throw;
            }
        }
    }
}