using freezer.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace freezer.Logic
{
    internal static class ListExtensions
    {
        public static List<T> InFreezer<T>(this List<T> list) where T : FoodItem
        {
            return list.Where (x=> x.Quantity > 0).ToList();
        }
    }
}
