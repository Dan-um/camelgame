using System;

namespace Camel
{
    class Program
    {
        // Function for if the user wants to drink from the canteen
        private static void DrinkFromCanteen(ref int playerThirst, ref int canteenDrinks)
        {
            // Make sure user has drinks
            if (canteenDrinks > 0)
            {
                Console.WriteLine("You drank from the canteen.");
                playerThirst = 0;
                canteenDrinks--;
            }
            // If the user has no drinks left
            else
            {
                Console.WriteLine("You have no water left!");
            }
        }

        // Function for if the user wants to move at a moderate pace
        private static void MoveAtModeratePace(ref int playerThirst, ref int camelFatigue, ref int canteenDrinks, ref int totalMilesTraveled, ref int pursuerMilesTraveled, Random rand)
        {
            // Movement of both player and pursuers are calculated
            int playerMovement = rand.Next(5, 12);
            totalMilesTraveled += playerMovement;
            pursuerMilesTraveled += rand.Next(7, 14);

            // Player thirst and camel fatigue increase
            playerThirst += 1;
            camelFatigue += 1;

            // Creating a chance for the user to find an oasis.
            int oasisChance = rand.Next(0, 100);

            // 5 percent chance to find oasis.
            if (oasisChance < 5)
            {
                Console.WriteLine("You found an oasis! You refill your canteen, and let your camel rest for a bit.");
                Console.WriteLine();
                playerThirst = 0;
                camelFatigue = 0;
                canteenDrinks = 3;
            }

            Console.WriteLine("You traveled" + " " + playerMovement + " " + "miles");
        }

        // Function for if the user wants to move at a fast pace across the desert
        private static void MoveAtFastPace(ref int playerThirst, ref int camelFatigue, ref int canteenDrinks, ref int totalMilesTraveled, ref int pursuerMilesTraveled, Random rand)
        {
            // Player and pursuer distance is calculated
            int playerMovement = rand.Next(10, 20);
            totalMilesTraveled += playerMovement;
            pursuerMilesTraveled += rand.Next(7, 14);

            // Player thirst and camel fatigue increases. Camel fatigue increases randomly, 1 to 3.
            int camelFatigueIncrease = rand.Next(1, 3);
            camelFatigue += camelFatigueIncrease;
            playerThirst++;

            // Creating a chance for the user to find an oasis.
            int oasisChance = rand.Next(0, 100);

            // 4 percent chance to find oasis.
            if (oasisChance < 4)
            {
                Console.WriteLine("You found an oasis! You refill your canteen, and let your camel rest for a bit.");
                Console.WriteLine();
                playerThirst = 0;
                camelFatigue = 0;
                canteenDrinks = 3;
            }

            Console.WriteLine("You traveled" + " " + playerMovement + " " + "miles");
        }

        private static void StopForTheNight(ref int camelFatigue, ref int pursuerMilesTraveled, Random rand)
        {
            // Print the feedback for user
            Console.WriteLine("You stopped for a while to rest.");
            Console.WriteLine("Your camel is happy!");

            // Reset camel tiredness. Move pursuers.
            camelFatigue = 0;
            pursuerMilesTraveled += rand.Next(7, 16);

        }

        // Function for if the user wants to check their status
        private static void CheckStatus(ref int totalMilesTraveled, ref int pursuerMilesTraveled, ref int canteenDrinks)
        {
            // Print out the information
            Console.WriteLine("Miles Traveled:" + " " + totalMilesTraveled);
            Console.WriteLine("Drinks left in canteen:" + " " + canteenDrinks);
            Console.WriteLine("The Natives are" + " " + (totalMilesTraveled - pursuerMilesTraveled) + " " + "miles behind you");
        }

        // Function for if the user wants to quit the game
        private static void QuitGame(ref bool done)
        {
            Console.WriteLine("You have quit the game. Thanks for playing!");
            done = true;
        }


        static void Main(string[] args)
        {
            // Instance variables
            int playerThirst = 0;
            int camelFatigue = 0;
            int totalMilesTraveled = 0;
            int pursuerMilesTraveled = -20;
            int canteenDrinks = 3;
            Random rand = new Random();

            // Introductory message
            Console.WriteLine("Welcome to Camel!");
            Console.WriteLine("You have stolen a camel to traverse the desert.");
            Console.WriteLine("The people you robbed are chasing you! Cross the desert before they catch up!");

            // Main game loop
            bool done = false;
            while (!done)
            {
                // Print commands
                Console.WriteLine();
                Console.WriteLine("A. Drink from your canteen.");
                Console.WriteLine("B. Ahead moderate speed.");
                Console.WriteLine("C. Ahead full speed.");
                Console.WriteLine("D. Stop and rest.");
                Console.WriteLine("E. Status check.");
                Console.WriteLine("Q. Quit.");

                // Get user command
                Console.Write("What is your command? ");
                string userCommand = Console.ReadLine();
                Console.WriteLine();

                // Process user command
                // If the user wants to drink from their canteen
                if (userCommand == "a")
                {
                    DrinkFromCanteen(ref playerThirst, ref canteenDrinks);
                }

                // If the user wants to move at moderate speed
                else if (userCommand == "b")
                {
                    MoveAtModeratePace(ref playerThirst, ref camelFatigue, ref canteenDrinks, ref totalMilesTraveled, ref pursuerMilesTraveled, rand);
                }

                // If the user wants to move at great speed
                else if (userCommand == "c")
                {
                    MoveAtFastPace(ref playerThirst, ref camelFatigue, ref canteenDrinks, ref totalMilesTraveled, ref pursuerMilesTraveled, rand);
                }

                // If the user wants to stop and rest
                else if (userCommand == "d")
                {
                    StopForTheNight(ref camelFatigue, ref pursuerMilesTraveled, rand);
                }

                // If the user wants to check their status
                else if (userCommand == "e")
                {
                    CheckStatus(ref totalMilesTraveled, ref pursuerMilesTraveled, ref canteenDrinks);
                }

                // Command to quit the game
                else if (userCommand == "q")
                {
                    QuitGame(ref done);
                }

                // If user enters unspecified command
                else
                {
                    Console.WriteLine("Unknown command.");
                }

                // Thirst mechanics
                if (!done & playerThirst > 4 & playerThirst <= 6)
                {
                    Console.WriteLine("You are thirsty!");
                }
                else if (playerThirst > 6)
                {
                    Console.WriteLine("You died of thirst!");
                    done = true;
                }

                // Winning the game mechanics
                if (totalMilesTraveled >= 200)
                {
                    Console.WriteLine("You crossed the desert and escaped! You win!");
                    done = true;
                }

                // Mechanics for if the pursuers catch the player
                if (!done & totalMilesTraveled - pursuerMilesTraveled <= 0)
                {
                    Console.WriteLine("You've been caught. You lose!");
                    done = true;
                }
                else if (totalMilesTraveled - pursuerMilesTraveled <= 15)
                {
                    Console.WriteLine("Your pursuers are closing in!");
                }

                // Camel fatigue mechanics
                if (!done & camelFatigue >= 5 & camelFatigue <= 8)
                {
                    Console.WriteLine("Your camel is getting tired.");
                }
                else if (camelFatigue > 8)
                {
                    Console.WriteLine("Your camel died of extreme fatigue. You lose!");
                    done = true;
                }

            }
        }

    }
}