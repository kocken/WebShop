using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt01
{
    class ShopItem : Product, IShopItem
    {
        private int amount;
        public int Amount
        {
            get
            {
                return amount;
            }
            set
            {
                if (value >= 0)
                {
                    amount = value;
                }
                else
                {
                    throw new Exception("Can't assign value " + value + ", value needs to be 0 or higher");
                }
            }
        }

        public string Summary {
            get
            {
                return Name + "[" + Id + "] - " +
                    Amount + "st - " +
                    Price + (Amount > 1 ? "kr/st " : "kr");
            }
        }

        public ShopItem(int id, string name, double price, bool ecoLabelled, int amount) : 
            base(id, name, price, ecoLabelled)
        {
            this.Amount = amount; 
        }
    }
}
