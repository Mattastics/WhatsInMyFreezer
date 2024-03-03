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

        public FoodItemRepository(IFoodItemRepository repository)
        {
            _dbContext = new FreezerContext();
        }

        public void AddFoodItem(FoodItem foodItem)
        {
            _dbContext.FoodItem.Add(foodItem);
            _dbContext.SaveChanges();
        }

        public FoodItem GetFoodItemById(int foodItemId)
        {
            return _dbContext.FoodItem.FirstOrDefault(x => x.FoodItemId == foodItemId);
        }
        public List<FoodItem> GetAllFoodItems()
        {
            return _dbContext.FoodItem.ToList();
        }


    }
}

