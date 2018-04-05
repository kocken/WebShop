using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt01
{
    class Discount
    {
        public string Code { get; private set; }
        public double Percent { get; private set; }

        public Discount(string code, double percent)
        {
            this.Code = code;
            this.Percent = percent;
        }

        public Discount(string data)
        {
            string[] splits = data.Split(',');
            if (splits.Length == 2)
            {
                try
                {
                    Code = splits[0];
                    if (string.IsNullOrEmpty(Code))
                    {
                        throw new ArgumentException($"Discount code \"{Code}\" can't be null or empty");
                    }
                    Percent = double.Parse(splits[1].Replace('.', ','));
                    if (Percent < 0 || Percent > 100)
                    {
                        throw new ArgumentException($"Discount percent value \"{Percent}\" must be within range of 0-100");
                    }
                }
                catch (FormatException)
                {
                    throw new FormatException($"Wrong data input - failed to format values of \"{data}\"");
                }
            }
            else
            {
                throw new ArgumentException($"Wrong data input - input \"{data}\" needs to have a single comma");
            }
        }

        public double DiscountedValue(double value)
        {
            return value * (1 - (Percent / 100));
        }
    }
}
