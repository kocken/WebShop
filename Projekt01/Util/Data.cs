using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt01
{
    class Data
    {
        public static string[] GetFileData(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Products file \"" + path + "\" was not found");
            }
            return File.ReadAllLines(path, Encoding.UTF7); // datan i filen
        }

        public static Product[] CsvDataToProductArray(string[] productData)
        {
            if (productData == null)
            {
                throw new ArgumentException("Product data argument \"" + productData + "\" is null");
            }
            if (productData.Length < 2)
            {
                throw new ArgumentException("Product data argument \"" + productData + "\" has no products, the string array has to be longer than one line");
            }
            List<Product> products = new List<Product>();
            for (int i = 1; productData.Length > i; i++) // tilldela 1 till i för att skipa första index (csv-förklaring)
            {
                products.Add(new Product(productData[i]));
            }
            return products.Cast<Product>().ToArray();
        }

        public static Discount[] CsvDataToDiscountArray(string[] discountData)
        {
            if (discountData == null)
            {
                throw new ArgumentException("Discount data argument \"" + discountData + "\" is null");
            }
            List<Discount> discounts = new List<Discount>();
            for (int i = 1; discountData.Length > i; i++) // tilldela 1 till i för att skipa första index (csv-förklaring)
            {
                discounts.Add(new Discount(discountData[i]));
            }
            return discounts.Cast<Discount>().ToArray();
        }
    }
}
