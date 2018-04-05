using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt01
{
    interface IProduct
    {
        int Id { get; }
        string Name { get; }
        double Price { get; }
        bool EcoLabelled { get; }
    }
}
