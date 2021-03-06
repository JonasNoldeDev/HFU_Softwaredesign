using System;
using System.Collections.Generic;

namespace Textadventure
{
    class Character : Thing
    {
        public static ConsoleColor Color = ConsoleColor.Green;
        public int InventorySize;
        public List<Item> Inventory = new List<Item>();
        public int Health;
        public int Attack;
        public bool Attackable = false;

        public void AddToInventory(Item itemToAdd)
        {
            itemToAdd.Owner = this;
            Inventory.Add(itemToAdd);
            if (this == Game.CurrentGame.Player) Console.WriteLine($"{itemToAdd.Name} has been added to your inventory.");
        }

        public void RemoveFromInventory(Item itemToRemove)
        {
            itemToRemove.Owner = null;
            Inventory.Remove(itemToRemove);
            if (this == Game.CurrentGame.Player) Console.WriteLine($"{itemToRemove.Name} has been removed from your inventory.");
        }

        public void Die()
        {
            CurrentArea.Things.Remove(this);
            CurrentArea = null;
            Console.WriteLine($"{Name} just died.");
        }
    }
}
