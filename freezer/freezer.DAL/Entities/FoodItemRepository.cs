using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


// Single Responsibility Principle: This class is responsible only for 
// data access logic, interacting with the datase and performing CRUD operations. 


namespace freezer.DAL.Entities
{
    public class FoodItemRepository : IFoodItemRepository
    {
        public readonly FreezerContext _dbContext;

        public FoodItemRepository(FreezerContext DbContext)
        {
            _dbContext = DbContext;
        }

        public FoodItem GetFoodItemByUPC(string upcCode)
        {
            return _dbContext.FoodItem.FirstOrDefault(f => f.UPC == upcCode);
        }


        public void AddFoodItems(List<FoodItem> foodItems)
        {
            foreach (var foodItem in foodItems)
            {
                // Check if the food item already exists by UPC
                var existingItem = _dbContext.FoodItem.SingleOrDefault(x => x.UPC == foodItem.UPC);
                if (existingItem != null)
                {
                    // If the item exists, just update the quantity
                    existingItem.Quantity += foodItem.Quantity;
                }
                else
                {
                    // If the item doesn't exist, add it to the context
                    _dbContext.FoodItem.Add(foodItem);
                }
            }
            // Save all changes once after processing all items
            _dbContext.SaveChanges();
        }

        public async Task<List<FoodItem>> GetAllFoodItemsAsync()
        {
            return await _dbContext.FoodItem.ToListAsync();
        }

        public List<FoodItem> GetAllFoodItems()
        {
            return _dbContext.FoodItem.ToList();
        }
        public List<string> GetAllCategories()
        {
            return _dbContext.FoodItem.Select(item => item.Category).Distinct().ToList();
        }


        public FoodItem GetFoodItemById(int foodItemId)
        {
            return _dbContext.FoodItem.SingleOrDefault(x => x.FoodItemId == foodItemId);
        }
        public async Task<List<FoodItem>> GetFoodItemsAsync()
        {
            return await _dbContext.FoodItem.ToListAsync();
        }


        public void UpdateFoodItem(FoodItem updatedFoodItem)
        {
            var item = _dbContext.FoodItem.Find(updatedFoodItem.FoodItemId);
            if (item != null)
            {
                item.Quantity = updatedFoodItem.Quantity;
                _dbContext.Update(item);
                _dbContext.SaveChanges(true);
            }

        }

        public FoodItem GetFoodItemByName(string name)
        {
            return _dbContext.FoodItem.FirstOrDefault(f => f.Name.ToLower() == name.ToLower());
        }

        public bool DoesFoodItemExist(int foodItemId)
        { return _dbContext.FoodItem.Any(item => item.FoodItemId == foodItemId); }

        public void RemoveFoodItems(List<FoodItem> itemsToRemove)
        {
            foreach (var itemModel in itemsToRemove)
            {
                var item = _dbContext.FoodItem.FirstOrDefault(f => f.FoodItemId == itemModel.FoodItemId);
                if (item != null && itemModel.Quantity <= item.Quantity)
                {
                    item.Quantity -= itemModel.Quantity;
                    if (item.Quantity >= 0)
                    {
                        _dbContext.FoodItem.Update(item);
                    }
                }
            }
            _dbContext.SaveChanges();
        }

        public void RemoveFoodItem(int foodItemId, int quantity)
        {
            var item = _dbContext.FoodItem.FirstOrDefault(f => f.FoodItemId == foodItemId);

            item.Quantity -= quantity;
            if (item.Quantity > 0)
            {
                _dbContext.FoodItem.Update(item);

            }
            else
            {
                _dbContext.FoodItem.Remove(item);
            }
            _dbContext.SaveChanges();
        }
    }
}

