using System;
using System.Collections.Generic;

namespace Textadventure
{
    class Thing
    {
        public String Name;
        public String Description;
        public String UniqueID = Helpers.NewUniqueID() + "";
        public Area CurrentArea;
        public Action Interaction;

        public void MoveToArea(Area areaToMove)
        {
            if (CurrentArea != null) CurrentArea.Things.Remove(this);
            CurrentArea = areaToMove;
            CurrentArea.Things.Add(this);
        }
    }
}
