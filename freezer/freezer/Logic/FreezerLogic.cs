using freezer.DAL.Migrations;
using freezer.Logic;
using freezer.Validators;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace freezer
{
    public class FreezerLogic : IFreezerLogic
    {
        private readonly IFoodItemRepository _foodItemRepo;

        public FreezerLogic(IFoodItemRepository foodItemRepository)
        {
            _foodItemRepo = foodItemRepository;
        }

        public void AddFoodItem(FoodItem foodItem)
        {
            var validator = new FoodItemValidator();
            if (validator.Validate(foodItem).IsValid)
            {
                _foodItemRepo.AddFoodItem(foodItem);
            }
            var existingItem = _foodItemRepo.GetFoodItemByName(foodItem.Name);
            if (existingItem != null)
            {
                existingItem.Quantity += foodItem.Quantity;
                _foodItemRepo.UpdateFoodItem(existingItem);
            }
            else
            { _foodItemRepo.AddFoodItem(foodItem);
            }
        }

        public List<FoodItem> GetFoodItems()
                {
                    return _foodItemRepo.GetAllFoodItems();
                }

        public FoodItem GetFoodItemById(int FoodItemId)
        {
            return _foodItemRepo.GetFoodItemById(FoodItemId);
        }

        public void UpdateFoodItem (FoodItem updatedFoodItem)
        {
            var validator = new FoodItemValidator();
            if (validator.Validate(updatedFoodItem).IsValid)
            {
                _foodItemRepo.UpdateFoodItem(updatedFoodItem);
            }    
        }  

        public FoodItem GetFoodItemByName (string FoodItemName)
        {
            return _foodItemRepo.GetFoodItemByName(FoodItemName);
        }

        public bool DoesFoodItemExist(int foodItemId)
        {
            return _foodItemRepo.DoesFoodItemExist(foodItemId);
        }

        //public void AddQuantityToFoodItem(string foodItemName, int quantityToAdd)
        //{
        //    var foodItem = _foodItemRepo.GetFoodItemByName(foodItemName);
        //    if (foodItem == null)
        //    {
        //        foodItem.Quantity += quantityToAdd;
        //        _foodItemRepo.UpdateFoodItem(foodItem);
                
        //    }
        //}

        public void RemoveQuantityFromFoodItem(string foodItemName, int quantityToRemove)
        {
            var foodItem = _foodItemRepo.GetFoodItemByName(foodItemName);
            if (foodItem != null)
            {
                _foodItemRepo.RemoveQuantityFromFoodItem(foodItem.FoodItemId, quantityToRemove);

                //Check if there is still more of this item. If not inform the user.
                if (!_foodItemRepo.DoesFoodItemExist(foodItem.FoodItemId))
                {
                    Console.WriteLine($"That was the last of the {foodItemName}. It has been removed.");
                }
            }
            else
            { Console.WriteLine("Item not found"); }    
        }

    }
}



