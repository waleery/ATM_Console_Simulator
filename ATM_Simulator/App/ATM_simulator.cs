using ATM_Simulator.App.UI;

namespace ATM_Simulator;


class ATM_simulator
{
    static void Main(string[] args)
    {
        AppScreen.Welcome();
        long cardNumber = Validator.Convert<long>("your card number.");
        Console.WriteLine($"Your card number is {cardNumber}");

        Utility.PressEnterToContinue();
    }
}

