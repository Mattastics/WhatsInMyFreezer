namespace freezer.DAL.Entities
{
    public interface IFoodItemRepository
    {
        FoodItem GetFoodItemByUPC(string upcCode);

        void AddFoodItems(List<FoodItem> foodItems);


        FoodItem GetFoodItemById(int foodItemid);

        public List<FoodItem> GetAllFoodItems();

        public void UpdateFoodItem(FoodItem updatedFoodItem);

        FoodItem GetFoodItemByName(string name);

        bool DoesFoodItemExist(int foodItemid);

        void RemoveFoodItems(List<FoodItem> itemsToRemove);

        void RemoveFoodItem(int foodItemid, int quantity);



    }
}
