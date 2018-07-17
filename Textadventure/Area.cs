using System;
using System.Collections.Generic;

namespace Textadventure
{
    class Area : Thing
    {
        public static ConsoleColor Color = ConsoleColor.Blue;
        public Dictionary<string, Area> Directions = new Dictionary<string, Area>();
        public List<Thing> Things = new List<Thing>();
    }
}
