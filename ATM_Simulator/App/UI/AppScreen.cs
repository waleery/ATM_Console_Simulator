using System;
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
        }

       
    }
}

