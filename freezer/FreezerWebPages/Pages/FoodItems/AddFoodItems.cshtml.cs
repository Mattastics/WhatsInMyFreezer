using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FreezerWebPages.Models;
using freezer.Logic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using freezer.DAL.Entities;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

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

            List<FoodItem> itemsNotFound = new List<FoodItem>();
            foreach (var foodItem in FoodItems)
            {
                if (!string.IsNullOrWhiteSpace(foodItem.UPC))
                {
                    (string Name, string Category) = await _freezerLogic.GetFoodItemNameByUPCAsync(foodItem.UPC);
                    if (Name != null)
                    {
                        foodItem.Name = Name;
                        foodItem.Category = Category ?? "Other"; // Assign "Other" if category is not provided
                    }
                    else
                    {
                        itemsNotFound.Add(foodItem); // Add to itemsNotFound list if UPC not found
                    }
                }
                else
                {
                    itemsNotFound.Add(foodItem); // Add to itemsNotFound list if UPC is empty or whitespace
                }
            }

            if (itemsNotFound.Any())
            {
                TempData["FoodItems"] = System.Text.Json.JsonSerializer.Serialize(itemsNotFound);
                return RedirectToPage("./RetryAddFoodItems");
            }
            else
            {
                await _freezerLogic.AddFoodItems(FoodItems); // Only add to DB if all items were found
                return RedirectToPage("./Success");
            }
        }
    }
}
