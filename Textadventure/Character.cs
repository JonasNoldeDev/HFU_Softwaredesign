using System;
using System.Collections.Generic;

namespace Textadventure
{
    class Character : Thing
    {
        public static ConsoleColor Color = ConsoleColor.Green;
        public int InventorySize;
        public List<Item> Inventory;
        public int Health;
        public int Attack;
        public bool Attackable = false;

        public void AddToInventory(Item itemToAdd)
        {
            itemToAdd.Owner = this;
            Inventory.Add(itemToAdd);
        }

        public void RemoveFromInventory(Item itemToRemove)
        {
            itemToRemove.Owner = null;
            Inventory.Remove(itemToRemove);
        }

        public void Die()
        {
            CurrentArea.Things.Remove(this);
            CurrentArea = null;
            Console.WriteLine($"{Name} just died.");
        }
    }
}
