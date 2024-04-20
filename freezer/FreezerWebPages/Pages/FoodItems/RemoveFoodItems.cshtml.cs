using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FreezerWebPages.Models;
using freezer.Logic;
using System.Security.Cryptography.X509Certificates;
using freezer;
using freezer.DAL.Entities;

namespace FreezerWebPages.Pages
{
    public class RemoveFoodItems : PageModel
    {
        private readonly IFreezerLogic _freezerLogic;


        [BindProperty]
        public RemoveFoodItemsRequest Request { get; set; }
        public List<FoodItem> FoodItems { get; set; }

        public RemoveFoodItems(IFreezerLogic freezerLogic, ILogger<RemoveFoodItems> logger)
        {
            _freezerLogic = freezerLogic;
        }

        public void OnGet()
        { 
           FoodItems = _freezerLogic.GetFoodItems();

        }

        public IActionResult OnPost()
        {

            _freezerLogic.RemoveFoodItem(Request.FoodItemId, Request.Quantity);
            return RedirectToPage("./Success");
        }
    }
}