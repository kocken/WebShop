using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt01
{
    interface IShopItem
    {
        int Amount { get; set; }
        string Summary { get; }
    }
}
