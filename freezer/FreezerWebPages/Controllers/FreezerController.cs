using Microsoft.AspNetCore.Mvc;
using freezer.DAL.Migrations;
using freezer.Logic;


namespace FreezerWebPages.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodItemsController(IFreezerLogic freezerLogic) : ControllerBase
    {

        private readonly IFreezerLogic _freezerLogic = freezerLogic;

        // GET: api/FoodItems
        [HttpGet]
        public IActionResult GetFoodItems()
        {
            var items = _freezerLogic.GetFoodItems();
            if (items == null || !items.Any())
            {
                return NotFound("No items found in the freezer.");
            }
            return Ok(items);
        }
        //POST: api/FoodItems
        [HttpPost]
        public IActionResult PostFoodItem([FromBody] FoodItem foodItem)
        {
            if (foodItem == null || string.IsNullOrEmpty(foodItem.Name) || foodItem.Quantity <= 0)
            { 
                return BadRequest("Invalid item or quantity");
            }

            _freezerLogic.AddFoodItem(foodItem);
            return Ok($"You have added {foodItem.Quantity} to {foodItem.Name}.");
        }


        // PATCH: api/FoodItems/add
        [HttpPatch("add")]
        public IActionResult AddItemQuantity([FromBody] FoodItem foodItem)
        {
            if (foodItem == null || string.IsNullOrEmpty(foodItem.Name) || foodItem.Quantity <= 0)
            {
                return BadRequest("Invalid item or quantity.");
            }

            _freezerLogic.AddFoodItem(foodItem);
            return Ok($"Added {foodItem.Quantity} to {foodItem.Name}.");
        }

        // PATCH: api/FoodItems/remove/name/quantity
        [HttpPatch("remove/{name}/{quantity}")]
        public IActionResult RemoveItemQuantity(string name, int quantity)
        {
            var foodItem = _freezerLogic.GetFoodItemByName(name);
            if (foodItem == null || quantity <= 0 || quantity > foodItem.Quantity)
            {
                return BadRequest("Invalid name or quantity.");
            }

            _freezerLogic.RemoveQuantityFromFoodItem(name, quantity);
            return Ok($"Removed {quantity} from {name}.");
        }

        ////DELETE: api/<FoodItems>/name/quantity
        //[HttpDelete("{name}/{quantity}")]
        //public IActionResult DeleteFoodItem(string name, int quantity)
        //{
        //    var foodItem = _freezerLogic.GetFoodItemByName(name);
        //    if (foodItem == null)
        //    {
        //        return NotFound($"You don't have any {name} in your freezer.");
        //    }
        //    if (quantity <=0)
        //    {
        //        return BadRequest("You can't have negative food.");
        //    }
        //    if (foodItem.Quantity  - quantity <= 0) {
        //        return Ok($"All of your {name} have been removed from your freezer."); 
        //    }

        //    return Ok($"{quantity} of {name} have been removed. You have: {foodItem.Quantity} remaining.");
        //}

    }
}
