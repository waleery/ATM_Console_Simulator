using System;
using ATM_Simulator.Domain.Entities;

namespace ATM_Simulator.App.UI
{
    public class AppScreen
    {
        public static void Welcome()
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
    }
}

