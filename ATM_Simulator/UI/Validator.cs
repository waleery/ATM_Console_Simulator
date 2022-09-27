using System;
using System.ComponentModel;

namespace ATM_Simulator.App.UI
{
    public static class Validator
    {
        // return converted data
        public static T Convert<T>(string prompt)
        {
            bool valid = false;
            string userInput;

            while (!valid)
            {
                userInput = Utility.GetUserInput(prompt);

                try
                {
                    //get type of input
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    if(converter != null)
                    {
                        return (T)converter.ConvertFromString(userInput);
                    }
                    //return default data to variable -> int = 0; string = null;
                    else
                    {
                        return default;
                    }
                }
                catch
                {
                    Utility.PrintMessage("Invalid input. Try again", false);
                }
            }
            return default;
        }
    }
}

