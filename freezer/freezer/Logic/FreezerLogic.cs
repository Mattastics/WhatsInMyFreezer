using freezer.DAL;
using freezer.DAL.Migrations;
using freezer.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace freezer.Logic
{
    internal class FreezerLogic : IFreezerLogic
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
        }

        public List<FoodItem> GetFoodItems()
                {
                    return _foodItemRepo.GetAllFoodItems();
                }

        public FoodItem GetFoodItemById(int FoodItemId)
        {
            return _foodItemRepo.GetFoodItemById(FoodItemId);
        }

        
    }
}

//{

//    internal class FoodItemService
//    {
//        private FreezerContext _context;

//        public FoodItemService(FreezerContext context)
//        {
//            _context = context;
//        }

//        // Create
//        public void AddFoodItem(FoodItem item)
//        {
//            _context.FoodItem.Add(item);
//            _context.SaveChanges();
//        }

//        public IEnumerable<FoodItem> GetAllFoodItems()
//        {
//            return _context.FoodItem.ToList();
//        }

//    }
//}


//// Update
//var firstItem = items.FirstOrDefault();
//if (firstItem != null)
//{
//    firstItem.Quantity += 1;
//    db.SaveChanges();
//}

//// Delete
//var lastItem = items.LastOrDefault();
//if (lastItem != null)
//{
//    db.FoodItem.Remove(lastItem);
//    db.SaveChanges();
//}
//}

