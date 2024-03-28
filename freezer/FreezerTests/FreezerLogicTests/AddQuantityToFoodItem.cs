using Xunit;
using freezer.Logic;
using freezer.DAL.Migrations;
using freezer;

namespace Freezer.Tests
{
    public class FreezerLogicTests
    {
        [Fact]
        public void AddFoodItem_ExistingItem_Success()
        {
            // Arrange
            var logic = new FreezerLogic();
            var existingItem = new FoodItem { Name = "Ice Cream", Quantity = 2 };
            logic.AddFoodItem(existingItem); // Add existing item to the freezer

            // Act
            var newItem = new FoodItem { Name = "Ice Cream", Quantity = 3 };
            logic.AddFoodItem(newItem); // Add more quantity to existing item
            var result = logic.GetFoodItemByName("Ice Cream");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Ice Cream", result.Name);
            Assert.Equal(5, result.Quantity); // Total quantity should be 5 (2 + 3)
        }
    }
}