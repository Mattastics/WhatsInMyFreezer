using freezer;
using freezer.Logic;
using freezer.DAL.Migrations;
using Xunit;



namespace FreezerTests.FreezerLogicTests
   
{
    public class FreezerLogicTests
    {
        [Fact]
        public void AddFoodItem_NewItem_Success()
        {
            // Arrange
            IFoodItemRepository foodItemRepository = new FoodItemRepository();
            var logic = new FreezerLogic(foodItemRepository);
            var newItem = new FoodItem { Name = "Ice Cream", Quantity = 3 };

            // Act
            logic.AddFoodItem(newItem);

            // Assert - Add your assertions here
        }
    }
}