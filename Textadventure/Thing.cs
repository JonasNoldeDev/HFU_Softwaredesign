using System;
using System.Collections.Generic;

namespace Textadventure
{
    class Thing
    {
        public String Name;
        public String Description;
        public Area CurrentArea;
        public String InteractionDescription;
        public Action Interaction;

        public void MoveToArea(Area areaToMove)
        {
            CurrentArea.Things.Remove(this);
            CurrentArea = areaToMove;
            CurrentArea.Things.Add(this);
        }
    }
}
