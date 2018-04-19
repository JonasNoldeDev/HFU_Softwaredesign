using System;
using System.Collections.Generic;
using System.Linq;

namespace Aufgabe_3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Beispiel: $ dotnet run program 10 2 13
            Console.WriteLine("From --------");
            Console.WriteLine("Basis: " + args[1]);
            Console.WriteLine("Wert:  " + args[3]);
            Console.WriteLine("To ----------");
            Console.WriteLine("Basis: " + args[2]);
            Console.WriteLine("Wert:  " + ConvertNumberToBaseFromBase(Int32.Parse(args[1]), Int32.Parse(args[2]), Int32.Parse(args[3])));
        }

        static int ConvertDecimalToHexal(int value)
        {
            int firstDigit = 0;
            int lastDigit = 0;

            while (value >= 6)
            {
                value -= 6;
                firstDigit++;
            }

            lastDigit = value;

            return firstDigit * 10 + lastDigit;
        }

        static int ConvertHexalToDezimal(int value)
        {
            int firstDigit = 0;
            int lastDigit = 0;

            while (value >= 10)
            {
                value -= 10;
                firstDigit++;
            }

            lastDigit = value;

            return firstDigit * 6 + lastDigit;
        }

        static int ConvertToBaseFromDecimal(int toBase, int value)
        {
            int fromBase = 10;
            int lastDigit = value % toBase;
            int firstDigit = (value - lastDigit) / toBase;

            return firstDigit * fromBase + lastDigit;
        }

        static int ConvertToDecimalFromBase(int fromBase, int value)
        {
            int toBase = 10;
            int lastDigit = value % toBase;
            int firstDigit = (value - lastDigit) / toBase;

            return firstDigit * fromBase + lastDigit;
        }

        static int ConvertNumberToBaseFromBase(int fromBase, int toBase, int value)
        {
            // Handle invalid input for "value"
            if (value < 0 || value > 1023)
            {
                Console.WriteLine("the third parameter (" + value + ") must lie between 0 and 1023");
                return 0;
            }

            // Handle invalid input for "fromBase" and "toBase"
            if (fromBase < 2 || fromBase > 10 || toBase < 2 || toBase > 10)
            {
                Console.WriteLine("the first two parameters (" + fromBase + " & " + toBase + ") must both lie between 2 and 10");
                return 0;
            }

            // Create list with the digits from "value"
            List<int> valueIntListFrom = new List<int>();
            foreach (char ch in value.ToString().ToCharArray())
            {
                valueIntListFrom.Add((int)Char.GetNumericValue(ch));
            }

            // Calculate the decimal value of the "value" according to "fromBase"
            int valueAsDecimal = 0;
            for (int i = 0; i < valueIntListFrom.Count; i++)
            {
                valueAsDecimal += valueIntListFrom[valueIntListFrom.Count - 1 - i] * (int)Math.Pow(fromBase, i);
            }

            // Create list with the digits for the value that will be returned
            List<int> valueIntListTo = new List<int>();
            valueIntListTo.Add(0);
            int currentExponent = 10; // is 10, because that is the highest exponent needed (1023 < 2^10)
            while (valueAsDecimal > 0)
            {
                int subtractValue = (int)Math.Pow(toBase, currentExponent);

                if ((valueAsDecimal - subtractValue) >= 0)
                {
                    valueAsDecimal -= subtractValue;
                    valueIntListTo[valueIntListTo.Count - 1]++;
                }
                else
                {
                    currentExponent--;
                    valueIntListTo.Add(0);
                }
            }

            // Converting the list of digits to one int value
            String returnValueAsString = "";
            Boolean numberStarts = false;
            foreach (int digit in valueIntListTo)
            {
                if (digit != 0)
                {
                    numberStarts = true;
                }

                if (numberStarts)
                {
                    returnValueAsString += digit;
                }
            }

            return Int32.Parse(returnValueAsString);
        }

    }
}
