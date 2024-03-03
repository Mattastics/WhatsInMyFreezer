using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace freezer.DAL.Migrations
{
    public interface IFoodItemRepository
    {
        void AddFoodItem (FoodItem foodItem);
        FoodItem GetFoodItemById (int foodItemid);

        public List<FoodItem> GetAllFoodItems ();
    }
}
