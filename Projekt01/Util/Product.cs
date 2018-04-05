using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt01
{
    class Product : IProduct
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public double Price { get; protected set; }
        public bool EcoLabelled { get; protected set; }

        public Product(int id, string name, double price, bool ecoLabelled)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.EcoLabelled = ecoLabelled;
        }

        public Product(string data)
        {
            string[] splits = data.Split(',');
            if (splits.Length == 4)
            {
                try
                {
                    Id = Int32.Parse(splits[0]);
                    if (Id < 0)
                    {
                        throw new ArgumentException($"Product id \"{Id}\" can't be a lower value than 0");
                    }
                    Name = Conversion.FirstCharToUpper(splits[1]);
                    if (String.IsNullOrEmpty(Name))
                    {
                        throw new ArgumentException($"Product name \"{Name}\" can't be null or empty");

                    }
                    Price = Double.Parse(splits[2].Replace(".", ","));
                    if (Price < 0)
                    {
                        throw new ArgumentException($"Product price \"{Price}\" can't be a lower value than 0");
                    }
                    EcoLabelled = bool.Parse(splits[3]);
                }
                catch (FormatException)
                {
                    throw new FormatException($"Wrong data input - failed to format values of \"{data}\"");
                }
            }
            else
            {
                throw new ArgumentException($"Wrong data input - input \"{data}\" needs to be a total of 3 commas per line");
            }
        }
    }
}
