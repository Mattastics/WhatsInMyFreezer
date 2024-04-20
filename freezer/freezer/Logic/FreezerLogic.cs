using freezer.DAL.Entities;
using freezer.Logic;
using freezer.Validators;
using FreezerWebPages.Services;

//Single Responsibility Priniciple: This class is responsible for the logic related to freezer inventory management such as 
//adding, updating, and retrieving food items, ensuring that only has one reason to change. 

//Dependency Inversion Principle: By depenting on the IFoodItemRepositoy interface, this class adheres to the
//Dependency Inversion Principle, allowing flexibility and east of testing. 

namespace freezer
{
public class FreezerLogic : IFreezerLogic
    {
        private readonly IFoodItemRepository _foodItemRepo;
        private readonly FoodDataService _foodDataService;

        public FreezerLogic(IFoodItemRepository foodItemRepository, FoodDataService foodDataService)
        {
            _foodItemRepo = foodItemRepository;
            _foodDataService = foodDataService;
        }

        public async Task<(string Name, string Category)> GetFoodItemNameByUPCAsync(string upcCode)
        {
            // Call the FoodDataService to get the name and category based on the UPC code
            return await _foodDataService.GetFoodItemNameByUPCAsync(upcCode);
        }


        public async Task AddFoodItems(List<FoodItem> foodItems)
        {
            foreach (var foodItem in foodItems)
            {
                var validator = new FoodItemValidator();
                var validationResult = validator.Validate(foodItem);
                if (validationResult.IsValid)
                {
                    // Check if the name is empty and there is a UPC to look up the name and category
                    if (string.IsNullOrWhiteSpace(foodItem.Name) && !string.IsNullOrWhiteSpace(foodItem.UPC))
                    {
                        var (name, category) = await _foodDataService.GetFoodItemNameByUPCAsync(foodItem.UPC);
                        foodItem.Name = name;
                        foodItem.Category = category;
                    }

                    // Check if there's an existing item by UPC in the repository
                    var existingItem = _foodItemRepo.GetFoodItemByUPC(foodItem.UPC);
                    if (existingItem != null)
                    {
                        existingItem.Quantity += foodItem.Quantity;
                        _foodItemRepo.UpdateFoodItem(existingItem);
                    }
                    else
                    {
                        // If no existing item, add the new one
                        _foodItemRepo.AddFoodItems(new List<FoodItem> { foodItem });
                    }
                }
                else
                {
                    // Handle validation errors
                    throw new InvalidOperationException(validationResult.Errors.Select(e => e.ErrorMessage).FirstOrDefault());
                }
            }
        }

        public Task AddFoodWithoutUPC(List<FoodItem> foodItems)
        {
            foreach (var foodItem in foodItems)
            {
                // Assume the name is provided by the user and validate the item
                var validator = new FoodItemValidator();
                var validationResult = validator.Validate(foodItem);

                if (validationResult.IsValid)
                {

                    // Check if an item with the same placeholder UPC already exists
                    var existingItem = _foodItemRepo.GetFoodItemByUPC(foodItem.UPC);
                    if (existingItem != null)
                    {
                        existingItem.Quantity += foodItem.Quantity;
                        _foodItemRepo.UpdateFoodItem(existingItem);
                    }
                    else
                    {
                        _foodItemRepo.AddFoodItems(new List<FoodItem> { foodItem });
                    }
                }
                else
                {
                    // Handle validation errors
                    throw new InvalidOperationException(validationResult.Errors.Select(e => e.ErrorMessage).FirstOrDefault());
                }
            }

            return Task.CompletedTask;
        }

        public List<string> GetCategories()
        {
            return _foodItemRepo.GetAllCategories();
        }


        public FoodItem GetFoodItemByUPC(string upcCode)
        {

            return _foodItemRepo.GetFoodItemByUPC(upcCode);
        }

        public async Task<List<FoodItem>> GetFoodItemsAsync()
        {
            return await _foodItemRepo.GetAllFoodItemsAsync();
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


        public void RemoveFoodItems(List<FoodItem> itemsToRemove)
        {
           
            _foodItemRepo.RemoveFoodItems(itemsToRemove);
        }


        public void RemoveFoodItem(int foodItemid, int quantity)
        {
            _foodItemRepo.RemoveFoodItem(foodItemid, quantity);
        }

    }
}



