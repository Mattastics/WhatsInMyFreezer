using System;
using Microsoft.EntityFrameworkCore.Design;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace freezer
{
    public class FoodItem
    {
        public int FoodItemId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public DateTime DateAdded { get; set; } 
        
        
    }
}
