using System;
using ATM_Simulator.Domain.Entities;

namespace ATM_Simulator.App.UI
{
    public class AppScreen
    {
        internal const string cur = " Zł"; 

        internal static void Welcome()
        {
            Console.WriteLine("---------Welcome to my ATM Simulator---------\n\n");
            Console.WriteLine("Please insert your ATM card");
            Console.WriteLine("Note: Actual ATM machine will accept and validate" +
                " a phisical ATM card, read the card number and validate it \n");

            Utility.PressEnterToContinue();
            Console.Clear();
        }

        //get card number and pin from user
        internal static UserAccount UserLoginForm()
        {
            UserAccount tempUserAccount = new UserAccount();

            tempUserAccount.CardNumber = Validator.Convert<long>("your card number.");
            tempUserAccount.CardPin = Convert.ToInt32(Utility.GetSecretInput("Enter your card PIN"));

            return tempUserAccount;
        }

        //animation of dots
        internal static void LoginProgress()
        {
            Console.WriteLine("\nChecking card number and PIN...");
            Utility.PrintDotAnimation();
        }

        internal static void PrintLockScreen()
        {
            Console.Clear();
            Utility.PrintMessage("You account is locked. Please go to the nearest branch" +
                " to unlock your account.", false);
            //exit app
            Environment.Exit(1);
        }

        internal static void WelcomeCustomer(string fullName)
        {
            Console.WriteLine($"Welcome back, {fullName}.");
            Utility.PressEnterToContinue();
        }

        internal static void DispalyAppMenu()
        {
            Console.Clear();
            Console.WriteLine("---------My ATM Simulator Menu---------\n");
            Console.WriteLine("1. Account Balance                     ");
            Console.WriteLine("2  Cash deposit                        ");
            Console.WriteLine("3. Withdrawl                           ");
            Console.WriteLine("4. Transfer                            ");
            Console.WriteLine("5. Transactions                        ");
            Console.WriteLine("6. Logout                              ");
        }

        internal static void LogoutProgress()
        {
            Console.WriteLine("Than You for using my ATM simulator.");
            Utility.PrintDotAnimation();
            Console.Clear(); 

        }

        internal static int SelectAmount()
        {
            Console.WriteLine("");
            Console.WriteLine(":1. 50 {0}        5.1000{0}",cur);
            Console.WriteLine(":2. 100{0}       6.2000{0}", cur);
            Console.WriteLine(":3. 200{0}       7.5000{0}", cur);
            Console.WriteLine(":4. 500",cur);
            Console.WriteLine(":0. Other amount");
            Console.WriteLine("");


            int selectedAmount = Validator.Convert<int>("option:");

            switch (selectedAmount)
            {
                case 1:
                    return 50;
                case 2:
                    return 100;
                case 3:
                    return 200;
                case 4:
                    return 500;
                case 5:
                    return 1000;
                case 6:
                    return 2000;
                case 7:
                    return 5000;
                case 0:
                    return 0;
                default:
                    Utility.PrintMessage("Invalid input. Try again.", false);
                    return -1;
            }
        }
    }

    
}

