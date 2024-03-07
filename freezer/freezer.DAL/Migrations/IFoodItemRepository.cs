namespace freezer.DAL.Migrations
{
    public interface IFoodItemRepository
    {
        void AddFoodItem(FoodItem foodItem);
        FoodItem GetFoodItemById(int foodItemid);

        public List<FoodItem> GetAllFoodItems();

        public void UpdateFoodItem(FoodItem updatedFoodItem);

        FoodItem GetFoodItemByName(string name);

        bool DoesFoodItemExist(int foodItemid);

        void RemoveQuantityFromFoodItem(int foodItemId, int quantityToRemove);


    }
}
