using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using freezer;
using System.Text.Json;
using System.Text.Json.Serialization;
using freezer.Logic;
using freezer.DAL.Entities;
using freezer.DAL;

var services = CreateServiceCollection();

var freezerLogic = services.GetService<IFreezerLogic>();

string userInput = DisplayMenuAndGetInput();

while (userInput.ToLower() != "e")
{


    if (userInput == "1")
    {
        //Create Item
        Console.WriteLine("What item do you want to add?");
        var name = Console.ReadLine();

        Console.WriteLine("How many items are you adding?");
        var quantity = int.Parse(Console.ReadLine());

        var foodItem = new FoodItem { Name = name, Quantity = quantity, DateAdded = DateTime.Now };
        freezerLogic.AddFoodItems(new List<FoodItem> { foodItem });
    }

    if (userInput == "2")
    {   //Reads Items
        var items = freezerLogic.GetFoodItems();
        foreach (var item in items)
        {
            Console.WriteLine($"Item: {item.Name}, Added: {item.DateAdded}, Quantity: {item.Quantity}.");
        }
    }

    if (userInput == "3")
    {
        // Shows the items in the freezer 
        var items = freezerLogic.GetFoodItems();
        Console.WriteLine("Your freezer has: ");
        foreach (var item in items)
        {
            Console.WriteLine($"Your freezer has {item.Quantity} {item.Name}.");
        }

        // List to hold items to remove
        List<FoodItem> itemsToRemove = new List<FoodItem>();

        // Flag to continue the loop
        bool removingItems = true;

        while (removingItems)
        {
            // Asks what item to remove
            Console.WriteLine("What are you removing? (Type 'done' when finished):");
            var nameToRemove = Console.ReadLine();

            // Break the loop if the user is done
            if (nameToRemove.Equals("done", StringComparison.OrdinalIgnoreCase))
            {
                removingItems = false;
                break;
            }

            var foodItem = freezerLogic.GetFoodItemByName(nameToRemove);
            if (foodItem != null)
            {
                Console.WriteLine($"How many {nameToRemove}s are you removing?");
                var quantityToRemove = int.Parse(Console.ReadLine());

                // Check if the quantity to remove is not greater than the current quantity 
                if (quantityToRemove <= foodItem.Quantity)
                {
                    // Update the quantity for the removal
                    foodItem.Quantity -= quantityToRemove;
                    itemsToRemove.Add(foodItem);
                }
                else
                {
                    Console.WriteLine("You don't have that many to remove!");
                }
            }
            else
            {
                Console.WriteLine("You don't have that in your freezer.");
            }
        }

        // Remove the items from the freezer
        if (itemsToRemove.Count > 0)
        {
            freezerLogic.RemoveFoodItems(itemsToRemove);
            Console.WriteLine("Items have been removed.");
        }
    }
    userInput = DisplayMenuAndGetInput();
}


static string DisplayMenuAndGetInput()
{
    Console.WriteLine("Press 1 to add itmes to your freezer.");
    Console.WriteLine("Press 2 to view the items to your freezer");
    Console.WriteLine("Press 3 to remove items to your freezer");
    Console.WriteLine("Press e to exit.");

    return Console.ReadLine();
}

static IServiceProvider CreateServiceCollection()
{
    return new ServiceCollection()
        .AddTransient<IFreezerLogic, FreezerLogic>()
        .AddTransient<IFoodItemRepository, FoodItemRepository>()
        .AddDbContext<FreezerContext>()
        .BuildServiceProvider();
}