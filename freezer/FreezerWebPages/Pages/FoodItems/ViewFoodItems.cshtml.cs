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

        public ViewFoodItemsModel(IFreezerLogic freezerLogic)
        {
            _freezerLogic = freezerLogic;
        }

        public void OnGet()
        {
            FoodItems = _freezerLogic.GetFoodItems();
        }
    }
}