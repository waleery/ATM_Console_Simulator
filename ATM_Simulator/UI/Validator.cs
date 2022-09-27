﻿using System;
using System.ComponentModel;

namespace ATM_Simulator.App.UI
{
    public static class Validator
    {
        public static T Convert<T>(string prompt)
        {
            bool valid = false;
            string userInput;

            while (!valid)
            {
                userInput = Utility.GetUserInput(prompt);

                try
                {
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    if(converter != null)
                    {
                        return (T)converter.ConvertFromString(userInput);
                    }
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

