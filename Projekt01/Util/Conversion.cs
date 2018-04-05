using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt01
{
    class Conversion
    {
        public static string FirstCharToUpper(string input)
        {
            if (input == null || input.Length == 0)
            {
                return input;
            }
            return input.Length == 1 ? 
                input.First().ToString().ToUpper() : input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}
