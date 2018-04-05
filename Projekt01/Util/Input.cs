using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt01.Util
{
    class Input
    {
        public static Discount GetDiscount(Discount[] discounts)
        {
            while (discounts != null && discounts.Length > 0)
            {
                string boxInput = Microsoft.VisualBasic.Interaction.
                    InputBox("Vill du använda en rabattkod?" + "\n" + "I så fall, ange denna här.", "Rabatt").ToLower(); // dialogruta för text-inmatning
                if (boxInput.Length == 0) // om användaren inte skrev in någon text
                {
                    return null;
                }
                else
                {
                    foreach (Discount discount in discounts)
                    {
                        if (boxInput.Equals(discount.Code.ToLower()))
                        {
                            MessageBox.Show("Du angav en giltig kod, " + discount.Percent + "% rabatt applicerad.", "Rabatt");
                            return discount;
                        }
                    }
                    MessageBox.Show("Du angav en felaktig kod, försök igen eller fortsätt utan rabattkod.", "Rabatt");
                }
            }
            return null;
        }
    }
}
