using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt01
{
    class Program
    {
        public static Product[] Products = Data.CsvDataToProductArray(Data.GetFileData("Data/Products.csv"));
        public static Discount[] Discounts = Data.CsvDataToDiscountArray(Data.GetFileData("Data/Discount_codes.csv"));
        public static Dictionary<int, ShopItem> Cart = new Dictionary<int, ShopItem>(); // int key = Product ID

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ShopWindow());
        }
    }
}
