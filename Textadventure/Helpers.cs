using System;
using System.Collections.Generic;

namespace Textadventure
{
    class Helpers
    {
        private static int _currentUniqueID = 0;
        public static void WriteLine(String text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Write(String text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static int NewUniqueID()
        {
            _currentUniqueID++;
            return _currentUniqueID;
        }
    }
}
