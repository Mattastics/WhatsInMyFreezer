using ConsoleApp1;

Console.WriteLine("How many records do you want to add? ");
var numberOfRecords = int.Parse(Console.ReadLine());

var recordList = new List<Dog>();
for (int i = 0; i < numberOfRecords; i++)
{
    // In this loop, populate the object's properties using Console.ReadLine()
    var Dog = new Dog();

    Console.WriteLine("What breed is the dog?");
    Dog.breed = Console.ReadLine();

    Console.WriteLine("What group of dog does it belong in? ");
    Dog.group = Console.ReadLine();


    Console.WriteLine("What color is the dog?");
    Dog.color = Console.ReadLine();
    recordList.Add(Dog);

    foreach (var record in recordList)
    {
        Console.WriteLine($"Dog breed: {Dog.breed}, Dog group: {Dog.group}, Dog color: {Dog.color}");
    }
}
// Print out the list of records using Console.WriteLine()