using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using FreezerWebPages.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using freezer.DAL.Entities;
using freezer.Logic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

namespace FreezerWebPages.Pages
{
    public class RetryAddFoodItemsModel : PageModel
    {
        private readonly IFreezerLogic _freezerLogic;
        private readonly ILogger<RetryAddFoodItemsModel> _logger;

        public RetryAddFoodItemsModel(IFreezerLogic freezerLogic, ILogger<RetryAddFoodItemsModel> logger)
        {
            _freezerLogic = freezerLogic;
            _logger = logger;
        }

        [BindProperty]
        public List<FoodItem> ItemsNotFound { get; set; }


        public void OnGet()
        {
            if (TempData["FoodItems"] != null)
            {
                ItemsNotFound = System.Text.Json.JsonSerializer.Deserialize<List<FoodItem>>(TempData["FoodItems"].ToString());
                TempData.Keep("FoodItems");
                foreach (var item in ItemsNotFound)
                {
                    Console.WriteLine($"Item Name: {item.Name}, UPC: {item.UPC}, QTY: {item.Quantity}, {item.DateAdded}");
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var item in ItemsNotFound)
            {
                // Generate a new placeholder UPC 
                item.UPC = GeneratePlaceholderUPC(item.Name);


                // Add the item
                await _freezerLogic.AddFoodWithoutUPC(new List<FoodItem> { item });
                _logger.LogInformation($"Item added: {item.Name} with UPC: {item.UPC}");
            }

            return RedirectToPage("./Success");
        }


        private string GeneratePlaceholderUPC(string itemName)
        {
            using (var sha256 = SHA256.Create())
            {
                // Compute the hash from the item name
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(itemName));
                // Convert the hash to a hexadecimal string
                var hashString = BitConverter.ToString(hash).Replace("-", "").Substring(0, 12);
                // Return a placeholder UPC with the TEMP prefix
                Console.WriteLine($"{hashString}");
                return $"TEMP{hashString}";
            
            }
        }
    }
}
