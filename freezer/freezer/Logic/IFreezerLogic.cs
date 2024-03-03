using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace freezer.Logic
{
    internal interface IFreezerLogic
    {
        ///Adds a product to the freezer
        ///<param name="foodItem"> The food that will be added.</param>
        public void AddFoodItem(FoodItem foodItem);

        ///Gets all the food in the freezer
        public List<FoodItem> GetFoodItems();

        ///Gets a product by the id of the product
        ///<param name="FoodItemID">The id given to the food</param>
        public FoodItem GetFoodItemById(int FoodItemId);
    }
}
