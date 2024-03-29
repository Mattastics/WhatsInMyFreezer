
"What's in My Freezer?" is an web application designed to help users manage and monitor their freezer inventory efficiently. Users can easily add items to their inventory, specifying details like the item's name, quantity, and the date it was added. The system supports adding items with or without a Universal Product Code (UPC), and if a UPC is provided, the application fetches the item name automatically to simplify the process.

"What's in My Freezer?" includes utilizing lists to store data inside a SQLite database and manage inventory data, enabling quick data retrieval and updates. The application is built as a CRUD API, allowing users to create, read, update, and delete inventory items seamlessly. Asynchronous programming is employed throughout the application, ensuring smooth and responsive user interactions, especially during data operations.
The project is also annotated with comments explaining the application of SOLID design principles. By incorporating these principles, "What's in My Freezer?" is structured to be robust, flexible, and scalable, catering to users' evolving needs while maintaining a clear and maintainable codebase.

Setting up the project:
1) Set up the database: In the command line, type 'dotnet ef database update'
2) Build the project: In the command line, type 'dotnet build'
3) Set startup program: Right click FreezerWebPages and select 'Set as Startup project.'
   
Adding items to your "What's in My Freezer?" inventory is a breeze, especially when it comes to manually entering the UPC code. If you're new to the term, UPC stands for Universal Product Code – it's that series of numbers accompanied by a barcode you see on almost every packaged item you purchase.

Here's a step-by-step guide to manually entering the UPC code:
1) Find the UPC Code: Look for the barcode on your food item's packaging. The UPC code is the 12-digit number right below it. If you're adding a bag of frozen peas or a pint of ice cream, you'll find it there!
2) Access the Inventory Page: Once you're on the "What's in My Freezer?" application, navigate to the page where you add new items to your inventory.
3) Enter the UPC Code: You'll see a text field labeled "UPC Code" or something similar. Simply type the 12-digit number you found on your item's packaging into this field. Then add the quantity you are adding and the date you are adding it. 
4) Submit: Once all the details are filled in, hit the submit button, and voilà, your item is added to your digital freezer inventory.

No UPC code? No Problem! The program will ask you the name the item you are putting in and then autogenerate a Temporary UPC-Code that will be tied to that items name so any time you add more of that item, it will be logged as the same item. 

The next steps in the development would be to add a Food Item category that is already pulled from the API and allow the user to manually chose a category if no UPC is taken in. This would help further organize the inventory into digestable chunks. Then, clean up some of the code before making the web page look more user-friendly. 

You may notice various references to a barcode scanner in the code. During its development, various barcode scanner APIs were explored to enable item addition via webcam scanning. However, due to technical limitations (I need a better webcam!), the focus shifted to manual UPC entry, keeping the system user-friendly and effective. Although the primary functionality now relies on manual entry, the foundational barcode scanning features remain within the application, allowing for future enhancements and integration as technology evolves.
