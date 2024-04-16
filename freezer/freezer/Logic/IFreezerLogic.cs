using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using freezer.DAL;
using freezer.DAL.Entities;
using freezer.Validators;


//Interface Segreation Prinicple: This class is speicific and client-oriented,
//meaning that any class implementing these interface will not be forced to use methods it does not need. 


namespace freezer.Logic
{
    public interface IFreezerLogic

    {

        Task<(string Name, string Category)> GetFoodItemNameByUPCAsync(string upcCode);


        /// <summary>
        /// Gets a food item by the UPC code.
        /// </summary>
        /// <param name="upcCode">The UPC code of the food item.</param>
        /// <returns>The food item with the given UPC code.</returns>
        FoodItem GetFoodItemByUPC(string upcCode);


        ///Adds items to the freezer
        ///<param name="foodItem"> The food that will be added.</param>
        Task AddFoodItems(List<FoodItem> foodItems);

        Task AddFoodWithoutUPC(List<FoodItem> foodItems);

        List<string> GetCategories();

        Task<List<FoodItem>> GetFoodItemsAsync();

        ///Gets all the food in the freezer
        List<FoodItem> GetFoodItems();


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

        void RemoveFoodItems(List<FoodItem> itemsToRemove);

        void RemoveFoodItem(int foodItemid, int quantity);


    }
}

