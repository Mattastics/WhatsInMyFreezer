using Microsoft.AspNetCore.Mvc.RazorPages;
using freezer.Logic;
using FreezerWebPages.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using freezer.DAL.Entities;

namespace FreezerWebPages.Pages
{
    public class ViewFoodItemsModel : PageModel
    {
        private readonly IFreezerLogic _freezerLogic;
        public List<FoodItem> FoodItems { get; set; }
        public string CurrentSort { get; set; } // To track current sort order
        public string SearchString { get; set; } // To hold the search query

        public ViewFoodItemsModel(IFreezerLogic freezerLogic)
        {
            _freezerLogic = freezerLogic;
        }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            CurrentSort = sortOrder;
            SearchString = searchString;

            FoodItems = await _freezerLogic.GetFoodItemsAsync();

            // Filter FoodItems based on the search string
            if (!string.IsNullOrEmpty(SearchString))
            {
                FoodItems = FoodItems.Where(f => f.Name.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
                                                 f.Category.Contains(SearchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Apply sorting
            FoodItems = sortOrder switch
            {
                "name" => FoodItems.OrderBy(f => f.Name).ToList(),
                "name_desc" => FoodItems.OrderByDescending(f => f.Name).ToList(),
                "date" => FoodItems.OrderBy(f => f.DateAdded).ToList(),
                "date_desc" => FoodItems.OrderByDescending(f => f.DateAdded).ToList(),
                "category" => FoodItems.OrderBy(f => f.Category).ToList(),
                "category_desc" => FoodItems.OrderByDescending(f => f.Category).ToList(),
                _ => FoodItems.OrderBy(f => f.Name).ToList(), // Default sorting
            };
        }
    }
}