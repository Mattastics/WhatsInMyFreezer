using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using freezer.DAL;
using freezer.DAL.Migrations;

namespace freezer.Logic
{
    public interface IFreezerLogic
    {
        ///Adds an item to the freezer
        ///<param name="foodItem"> The food that will be added.</param>
        public void AddFoodItem(FoodItem foodItem);

        ///Gets all the food in the freezer
        public List<FoodItem> GetFoodItems();

        ///Gets a item by the id of the product
        ///<param name="FoodItemID">The id given to the food</param>
        public FoodItem GetFoodItemById(int FoodItemId);

        ///Removes items from the freezer
        ///<param name="foodItem"> The food that will be added</param>   
        public void UpdateFoodItem(FoodItem updatedFoodItem);

       
        /// Gets an item by the name of the product. 
        /// <param name="Name"></param>
        public FoodItem GetFoodItemByName(string Name);

 
        /// Check if a food item exists in the freezer
        /// <param name="foodItemId"></param>
        bool DoesFoodItemExist(int foodItemId);

  
        /// Removes a speicified quantity of a food item from the freezer. 
        /// <param name="name"></param>
        /// <param name="quantityToRemove"></param>
      
        public void RemoveQuantityFromFoodItem(string name, int quantityToRemove);
    }
}
