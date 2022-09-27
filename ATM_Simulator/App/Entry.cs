using System;
using ATM_Simulator.App.UI;

namespace ATM_Simulator.App
{
    class Entry
    {
        static void Main(string[] args)
        {
            AppScreen.Welcome();
            ATM_simulator ATM_Simulator = new ATM_simulator();
            ATM_Simulator.InitializeData();
            ATM_Simulator.ChcekUserCardNumAndPassword();
            ATM_Simulator.Welcome();

            //Utility.PressEnterToContinue();
        }
    }
}

