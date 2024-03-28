using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using freezer.Logic;
using System.Collections.Generic;
using freezer.DAL.Entities;

namespace FreezerWebPages.Pages
{

    public class SuccessModel : PageModel
    {

        private readonly IFreezerLogic _freezerLogic;
        public List<FoodItem> FoodItems { get; set; }

        public SuccessModel(IFreezerLogic freezerLogic)
        {
            _freezerLogic = freezerLogic;
        }

        public void OnGet()
        {
            FoodItems = _freezerLogic.GetFoodItems();
        }
    }
}