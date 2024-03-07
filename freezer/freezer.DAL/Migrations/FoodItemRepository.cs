using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace freezer.DAL.Migrations
{
    public class FoodItemRepository : IFoodItemRepository
    {
        private readonly FreezerContext _dbContext;

        public FoodItemRepository(FreezerContext DbContext)
        {
            _dbContext = DbContext;
        }

        public void AddFoodItem(FoodItem foodItem)
        {
            _dbContext.FoodItem.Add(foodItem);
            _dbContext.SaveChanges();
        }

        public FoodItem GetFoodItemById(int foodItemId)
        {
            return _dbContext.FoodItem.SingleOrDefault(x => x.FoodItemId == foodItemId);
        }
        public List<FoodItem> GetAllFoodItems()
        {
            return _dbContext.FoodItem.ToList();
        }

        public void UpdateFoodItem(FoodItem updatedFoodItem)
        {
            var item = _dbContext.FoodItem.Find(updatedFoodItem.FoodItemId);
            if (item != null)
            {
                item.Quantity = updatedFoodItem.Quantity;

            }
        }

        public FoodItem GetFoodItemByName(string name)
        {
            return _dbContext.FoodItem.FirstOrDefault(f => f.Name.ToLower() == name.ToLower());
        }

        public bool DoesFoodItemExist(int foodItemId)
        { return _dbContext.FoodItem.Any(item => item.FoodItemId == foodItemId);}

        public void RemoveQuantityFromFoodItem(int foodItemId, int quantityToRemove)
        {
            var item = _dbContext.FoodItem.Find(foodItemId);
            if (item != null && quantityToRemove <= item.Quantity)
            {
                item.Quantity -= quantityToRemove;
                if (item.Quantity == 0)
                {
                    _dbContext.FoodItem.Remove(item); 
                }
                _dbContext.SaveChanges();
            }
        }


    }
}

