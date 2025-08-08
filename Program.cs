// COMP1202 Assignment #2 - Group 9
// Group Members:
// Student 1: Disha Padsala, ID: 101581979]
// Student 2: Maria Tai, ID: 101563558]
// Student 3: Genesis Tugawin ID: 101579615]


namespace Group09GameInventory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Initialize video game list inventory
            Game[] games = new Game[9999];
            int gameCount = 0; //Counter for number of games in the inventory


            Console.WriteLine("*** Group 9: Video Game Shop Inventory Management System ***\n");

            //Load existing list of video games inventory from file
            gameCount = LoadGameInventory(games);

            //Initialize exit flag for main menu loop
            bool exitProgram = false;

            //Main menu loop
            while (!exitProgram)
            {
                DisplayMenu();

                //Get user choice from menu
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllGames(games, gameCount);
                        break;
                    case "2":
                        gameCount = AddNewGame(games, gameCount);
                        break;
                    case "3":
                        SearchByItemNumber(games, gameCount);
                        break;
                    case "4":
                        SearchByMaxPrice(games, gameCount);
                        break;
                    case "5":
                        PerformStatisticalAnalysis(games, gameCount);
                        break;
                    case "6":
                        exitProgram = true;
                        Console.WriteLine("\nThank you for using Group 9: Video Game Shop Inventory Management System!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static int LoadGameInventory(Game[] games)
        {
            int gameCount = 0;


            Console.WriteLine("\nLoading game inventory from file...");

            try
            {
                //Attempt to read the VideoGames.txt file
                StreamReader gameFile = new StreamReader("VideoGames.txt");

                //Read the entire file content
                string gameData = gameFile.ReadToEnd();

                //Close the file after reading
                gameFile.Close();

                //Split the file content by new line to get each game entry
                string[] gameEntries = gameData.Split('\n');

                foreach (string entry in gameEntries)
                {
                    //Split each entry by comma to get individual attributes
                    string[] attributes = entry.Split(',');

                    //Check if we have the correct number of attributes
                    if (attributes.Length == 5)
                    {
                        //Create a new Game object
                        Game game = new();

                        //Set the attributes for the game object
                        game.SetItemNumber(int.Parse(attributes[0]));
                        game.SetItemName(attributes[1]);
                        game.SetPrice(double.Parse(attributes[2]));
                        game.SetUserRating(int.Parse(attributes[3]));
                        game.SetQuantity(int.Parse(attributes[4]));

                        //Add each video game to array
                        games[gameCount] = game;
                        gameCount++;
                    }

                }

                //Show success in reading the file
                Console.WriteLine("Video game inventory list loaded successfully.");

            }
            catch (Exception ex)
            {
                //Show error message if file not found or any other issue
                Console.WriteLine($"Error loading video game list inventory: {ex.Message}");
            }
            return gameCount;
        }

        //Display the main menu
        static void DisplayMenu()
        {
            Console.WriteLine("\n--- Main Menu ---");
            Console.WriteLine("1. Display all items");
            Console.WriteLine("2. Add a new game");
            Console.WriteLine("3. Search by item number");
            Console.WriteLine("4. Search by maximum price");
            Console.WriteLine("5. Perform statistical analysis");
            Console.WriteLine("6. Exit\n");
            Console.Write("Please select an option (1-6): ");

        }

        //Display all games in inventory
        static void DisplayAllGames(Game[] games, int gameCount)
        {
            Console.WriteLine("\n=== All Video Games in Inventory ===\n");

            if (gameCount == 0)
            {
                Console.WriteLine("\nNo games in inventory.");
            }

            for (int i = 0; i < gameCount; i++)
            {
                Console.WriteLine(games[i].DisplayDetails());
            }
            Console.WriteLine($"\nTotal Video Games: {gameCount}");
        }

        //Add a new game to inventory
        static int AddNewGame(Game[] games, int gameCount)
        {
            Console.WriteLine("\n=== Add New Video Game ===");

            if (gameCount >= 10000)
            {
                Console.WriteLine("Inventory is full. Cannot add more games.");
            }

            //Create a new Game object
            Game game = new Game();

            // Get item number
            Console.Write("\nDo you know the Item Number? (y/n): ");
            string response = Console.ReadLine();

            if (response == "y")
            {
                Console.Write("\nEnter 4-digit Item Number: ");

                int itemNumber = 0;

                bool invalidItemNumber = true;

                //Validate item number input
                while (invalidItemNumber)
                {
                    if (int.TryParse(Console.ReadLine(), out itemNumber))
                    {
                        if (itemNumber >= 1 && itemNumber <= 9999)
                        {
                            //Check if item number already exists
                            bool exists = false;
                            for (int i = 0; i < gameCount; i++)
                            {
                                if (games[i].GetItemNumber() == itemNumber)
                                {
                                    Console.Write("\nItem Number already exists. Please enter a different number: ");
                                    exists = true;
                                }
                            }
                            if (!exists) invalidItemNumber = false;
                        }
                        else
                        {
                            Console.Write("\nInvalid Item Number. Please enter a number between 1 and 9999: ");
                            invalidItemNumber = true;
                        }
                    }
                }

                game.SetItemNumber(itemNumber);
            }
            else
            {
                Random rnd = new Random();
                game.SetItemNumber(rnd.Next(1, 9999));
                Console.WriteLine($"\nGenerated Item Number: {game.GetItemNumber()}");
            }


            //Get item name
            Console.Write("\nEnter item name: ");
            game.SetItemName(Console.ReadLine());


            //Get price
            Console.Write("\nEnter Price: ");

            //Validate price input
            double itemPrice = 0;

            bool invalidPrice = true;

            //Validate item number input
            while (invalidPrice)
            {
                if (double.TryParse(Console.ReadLine(), out itemPrice))
                {
                    invalidPrice = false;
                }
                else
                {
                    Console.Write("\nInvalid Item Price. Please enter a price between 0.01 and 99999.99: ");
                    invalidPrice = true;
                }
            }

            game.SetPrice(itemPrice);


            //Get user rating
            Console.Write("\nEnter User Rating (1-5): ");

            string ratingInput;

            bool invalidRating = true;

            //Validate user rating input
            while (invalidRating)
            {
                ratingInput = Console.ReadLine();

                if (ratingInput == "1" || ratingInput == "2" || ratingInput == "3" || ratingInput == "4" || ratingInput == "5")
                {

                    game.SetUserRating(int.Parse(ratingInput));
                    invalidRating = false;
                }
                else
                {
                    Console.Write("\nInvalid User Rating. Please enter a rating between 1 and 5: ");
                    invalidRating = true;
                }
            }

            //Get quantity
            Console.Write("\nEnter Quantity: ");

            int itemQuantity = 0;

            bool invalidQuantity = true;

            //Validate quantity input
            while (invalidQuantity)
            {
                if (int.TryParse(Console.ReadLine(), out itemQuantity))
                {
                    invalidQuantity = false;
                }
                else
                {
                    Console.Write("\nInvalid Item Quantity. Please enter a quantity between 1 and 9999: ");
                    invalidQuantity = true;
                }
            }

            game.SetQuantity(itemQuantity);


            //Add to inventory and save
            games[gameCount] = game;
            gameCount++;

            SaveInventory(games, gameCount);

            Console.WriteLine("\nGame added successfully!");
            Console.WriteLine(game.ToString());

            return gameCount;
        }

        //Save Game array data to VideoGames.txt file
        static void SaveInventory(Game[] games, int gameCount)
        {
            try
            {
                StreamWriter outputFile = new StreamWriter("VideoGames.txt", true);

                for (int i = 0; i < gameCount; i++)
                {
                    outputFile.WriteLine(games[i].ToString());
                }

                outputFile.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving inventory: {ex.Message}");
            }
        }

        static void SearchByItemNumber(Game[] games, int gameCount)
        {
            Console.Write("\nEnter Item Number to search: ");
            int number = int.Parse(Console.ReadLine());
            bool notFound = true;

            for (int i = 0; i < gameCount; i++)
            {
                if (games[i].GetItemNumber() == number)
                {
                    Console.WriteLine("\nItem found:\n");
                    Console.WriteLine(games[i].DisplayDetails());
                    notFound = false;
                }
            }

            if (notFound) Console.WriteLine("\nItem not found.");
        }

        //Search for games under a maximum price
        static void SearchByMaxPrice(Game[] games, int gameCount)
        {
            Console.Write("\nEnter maximum price: ");
            double maxPrice = double.Parse(Console.ReadLine());

            Console.WriteLine($"\n=== Games under ${maxPrice} ===\n");
            bool notFound = true;
            for (int i = 0; i < gameCount; i++)
            {
                // Check if game price is less than or equal to maxPrice
                if (games[i].GetPrice() <= maxPrice)
                {
                    Console.WriteLine(games[i].DisplayDetails());
                    notFound = false;
                }
            }

            if (notFound) Console.WriteLine("\nNo games found under that price.");
        }


        //Perform statistical analysis on inventory
        static void PerformStatisticalAnalysis(Game[] games, int gameCount)
        {
            Console.WriteLine("\n=== Statistical Analysis ===");

            if (gameCount == 0)
            {
                Console.WriteLine("\nNo games in inventory for analysis.");
            }

            // Calculate mean price
            double totalPrice = 0;
            double minPrice = games[0].GetPrice();
            double maxPrice = games[0].GetPrice();
            Game cheapestGame = games[0];
            Game expensiveGame = games[0];

            for (int i = 0; i < gameCount; i++)
            {
                double price = games[i].GetPrice();
                totalPrice = totalPrice + price;

                if (price < minPrice)
                {
                    minPrice = price;
                    cheapestGame = games[i];
                }

                if (price > maxPrice)
                {
                    maxPrice = price;
                    expensiveGame = games[i];
                }
            }

            double meanPrice = totalPrice / gameCount;

            Console.WriteLine($"\nMean video game price: ${meanPrice.ToString("F2")}");
            Console.WriteLine($"Video game price range: ${minPrice.ToString("F2")} - ${maxPrice.ToString("F2")}");
            Console.WriteLine($"Cheapest video game item: {cheapestGame.GetItemName()} - ${minPrice.ToString("F2")}");
            Console.WriteLine($"Most expensive video game item: {expensiveGame.GetItemName()} - ${maxPrice.ToString("F2")}");
        }



    }

    //Class Game
    internal class Game
    {
        //Fields for game attributes
        private int itemNumber;
        private string itemName;
        private double price;
        private int userRating;
        private int quantity;

        //Constructor with all parameters
        public Game(int number, string name, double cost, int rating, int units)
        {
            itemNumber = number;
            itemName = name;
            price = cost;
            userRating = rating;
            quantity = units;
        }

        public Game()
        {
        }

        //Accessor methods(getters)
        public int GetItemNumber()
        {
            return itemNumber;
        }

        public string GetItemName()
        {
            return itemName;
        }

        public double GetPrice()
        {
            return price;
        }

        public int GetUserRating()
        {
            return userRating;
        }

        public int GetQuantity()
        {
            return quantity;
        }

        //Mutator methods(setters)
        public void SetItemNumber(int number)
        {
            itemNumber = number;
        }

        public void SetItemName(string name)
        {
            itemName = name;
        }

        public void SetPrice(double cost)
        {
            price = cost;
        }

        public void SetUserRating(int rating)
        {
            userRating = rating;
        }

        public void SetQuantity(int units)
        {
            quantity = units;
        }

        //Override ToString method to format game data for file
        public override string ToString()
        {
            string entry = itemNumber + "," + itemName + "," + price + "," + userRating + "," + quantity;
            return entry;
        }

        //Method to format game entry for display
        public string DisplayDetails()
        {
            //int test = 43;
            string itemNumberString = itemNumber.ToString();
            for (int i = itemNumberString.Length; i < 4; i++)
            {
                itemNumberString = " " + itemNumberString; // Pad with leading spaces to ensure 4 digits
            }

            string itemNameString = itemName;
            for (int i = itemNameString.Length; i < 30; i++)
            {
                itemNameString += " "; // Pad with trailing spaces to ensure 30 characters
            }

            string priceString = price.ToString("C2");
            for (int i = priceString.Length; i < 10; i++)
            {
                priceString = " " + priceString; // Pad with leading spaces to ensure 10 characters
            }


            string displayEntry = $"No: {itemNumberString}   Name: {itemNameString}   Price: {priceString}   User Rating: {userRating}   Quantity: {quantity}";
            return displayEntry;
        }


    }


}
