// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using freezer;
using freezer.DAL;
using freezer.DAL.Migrations;
using System.Text.Json;
using System.Text.Json.Serialization;
using freezer.Logic;

var services = CreateServiceCollection();

var freezerLogic = services.GetService<IFreezerLogic>();
 
string userInput = DisplayMenuAndGetInput();

while (userInput.ToLower() != "e");
using (var context = new FreezerContext())


    if (userInput == "1")
    {
        //Create Item
        Console.WriteLine("What item do you want to add?");
        var userInputAsJson = Console.ReadLine();
        var FoodItem = JsonSerializer.Deserialize<FoodItem>(userInputAsJson);
        freezerLogic.AddFoodItem(FoodItem);
    }

    if (userInput == "2")
    {   //Reads Items
        var items = freezerLogic.GetFoodItems();
        foreach (var item in items)
        {
            Console.WriteLine($"Item: {item.Name}, Added: {item.DateAdded}, Quantity: {item.Quantity}.");
        }
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
        .BuildServiceProvider();
}
