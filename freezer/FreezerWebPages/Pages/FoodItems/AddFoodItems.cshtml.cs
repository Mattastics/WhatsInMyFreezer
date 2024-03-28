using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FreezerWebPages.Models;
using freezer.Logic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using freezer.DAL.Entities;
using Microsoft.Extensions.Logging;

namespace FreezerWebPages.Pages
{
    public class AddFoodItemsModel : PageModel
    {
        private readonly IFreezerLogic _freezerLogic;

        private readonly ILogger<AddFoodItemsModel> _logger;

        [BindProperty]
        public List<FoodItem> FoodItems { get; set; } = new List<FoodItem>();

        public AddFoodItemsModel(IFreezerLogic freezerLogic, ILogger<AddFoodItemsModel> logger)
        {
            _freezerLogic = freezerLogic;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || FoodItems == null || !FoodItems.Any())
            {
                ModelState.AddModelError("", "No items provided.");
                return Page(); 
            }

            bool allItemsFound = true;

            foreach (var foodItem in FoodItems)
            {
                Console.WriteLine(foodItem.UPC);

                if (!string.IsNullOrWhiteSpace(foodItem.UPC))
                {
                    var itemName = await _freezerLogic.GetFoodItemNameByUPCAsync(foodItem.UPC);
                    if (!string.IsNullOrWhiteSpace(itemName))
                    {
                        foodItem.Name = itemName;
                    }
                    else
                    {
                        allItemsFound = false;
                        foodItem.Name = "Unknown Item"; // Placeholder for unknown items.
                        _logger.LogInformation($"Item not add: {foodItem.Name}");
                    }
                }
                else
                {
                    // If UPC is empty or whitespace, consider the item as not found
                    allItemsFound = false;
                    foodItem.Name = "Unknown Item"; // Placeholder for items without a UPC.
                }
            }

            if (allItemsFound)
            {
                await _freezerLogic.AddFoodItems(FoodItems);
                return RedirectToPage("./Success");
            }
            else
            {
                // Serialize the items that were not found
                TempData["FoodItems"] = System.Text.Json.JsonSerializer.Serialize(FoodItems.Where(item => item.Name == "Unknown Item").ToList());
                return RedirectToPage("./RetryAddFoodItems");
            }
        }
    }
}
