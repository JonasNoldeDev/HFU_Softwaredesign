using System;
using System.Collections.Generic;

namespace Textadventure
{
    class GamePresets
    {
        public static void Preset1()
        {
            #region Initialize and Connect Areas
                Area alcimedesShack = new Area()
                {
                    Name = "Alcimedes' Shack",
                    Description = "..."
                };
                Area colorfulFields = new Area()
                {
                    Name = "The Colorful Fields",
                    Description = "..."
                };
                Area deepCaves = new Area()
                {
                    Name = "The Deep Caves",
                    Description = "..."
                };
                Area infiniteWoodlands = new Area()
                {
                    Name = "The Infinite Woodlands",
                    Description = "..."
                };
                Area monastery = new Area()
                {
                    Name = "The Monastery",
                    Description = "..."
                };

                alcimedesShack.Directions = new Area[]{infiniteWoodlands, null, null, colorfulFields};
                colorfulFields.Directions = new Area[]{monastery, alcimedesShack, null, null};
                deepCaves.Directions = new Area[]{null, null, alcimedesShack, infiniteWoodlands};
                infiniteWoodlands.Directions = new Area[]{null, deepCaves, alcimedesShack, monastery};
                monastery.Directions = new Area[]{null, infiniteWoodlands, colorfulFields, null};
            #endregion

            #region Initialize Items
                Item yellowHerb = new Item()
                {
                    Name = "Yellow Herb",
                    Description = "..."
                };
                Item blueHerb = new Item()
                {
                    Name = "Blue Herb",
                    Description = "..."
                };
                Item redHerb = new Item()
                {
                    Name = "Red Herb",
                    Description = "..."
                };
                Item whiteHerb = new Item()
                {
                    Name = "White Herb",
                    Description = "..."
                };
                Item magicRod = new Item()
                {
                    Name = "Magic Rod",
                    Description = "..."
                };
                Item medicine = new Item()
                {
                    Name = "Herbal Medicine",
                    Description = "..."
                };
            #endregion

            #region Initialize Objects
                Object kettle = new Object()
                {
                    Name = "Yellow Herb",
                    Description = "..."
                };
            #endregion

            #region Initialize Characters
                Character player = new Character()
                {
                    Name = "Player",
                    Description = "a young druid's apprentice",
                    InventorySize = 4,
                    Health = 10,
                    Attack = 10
                };
                Character alcimedes = new Character()
                {
                    Name = "Alcimedes",
                    Description = "an old druid, whose motto is 'a liquor a day keeps the doctor away'",
                    InventorySize = 10,
                    Health = 5,
                    Attack = 0
                };
                Character mary = new Character()
                {
                    Name = "Mary",
                    Description = "a beautiful flower maiden with curly blonde hair",
                    InventorySize = 10,
                    Health = 10,
                    Attack = 0
                };
                Character bear = new Character()
                {
                    Name = "The grizzly",
                    Description = "a big gray grizzly bear",
                    InventorySize = 10,
                    Health = 15,
                    Attack = 3,
                    Attackable = true
                };
                Character monk = new Character()
                {
                    Name = "Mohammed Lee",
                    Description = "a misterious monk",
                    InventorySize = 10,
                    Health = 10,
                    Attack = 6,
                    Attackable = true
                };
            #endregion

            #region Place Things on the Areas
                player.MoveToArea(alcimedesShack);
                alcimedes.MoveToArea(alcimedesShack);
                kettle.MoveToArea(alcimedesShack);

                mary.MoveToArea(colorfulFields);
                yellowHerb.MoveToArea(colorfulFields);

                blueHerb.MoveToArea(deepCaves);

                bear.MoveToArea(infiniteWoodlands);
                //wood.MoveToArea(infiniteWoodlands);
                redHerb.MoveToArea(infiniteWoodlands);

                monk.MoveToArea(monastery);
                whiteHerb.MoveToArea(monastery);
            #endregion

            #region Initialize Game Steps
                GameStep[] gameSteps = new GameStep[] 
                {
                    new GameStep(
                        Game.CurrentGame.Player.Inventory.Contains(magicRod),
                        "Talk to Alcimedes."
                    )
                    // get fire wood ?
                    // get all herbs
                    // brew medicine
                };
            #endregion
            
            #region Add Interactions
                void kettleInteraction()
                {
                    bool allHerbsAvailable = true;
                    Item[] requiredHerbs = new Item[]{ yellowHerb, blueHerb, redHerb, whiteHerb };
                    String missingHerbsMessage  = "The following herbs are missing for brewing the medicine:\n";
                    foreach (var herb in requiredHerbs)
                    {
                        if (!Game.CurrentGame.Player.Inventory.Contains(herb))
                        {
                            allHerbsAvailable = false;
                            missingHerbsMessage += herb.Name + "\n";
                        }
                    }
                    missingHerbsMessage += "I have to find them and come back here again.";
                    if (allHerbsAvailable)
                    {
                        if (Game.CurrentGame.Player.InventorySize < Game.CurrentGame.Player.Inventory.Count)
                        {
                            foreach (var herb in requiredHerbs)
                            {
                                Game.CurrentGame.Player.Inventory.Remove(herb);
                            }
                            Game.CurrentGame.Player.Inventory.Add(medicine);
                            Console.WriteLine("All the herbs are gone.. But finally I have the medicine for Alcimedes!");
                        }
                        else
                        {
                            Console.WriteLine("Inventory is full.");
                        }
                    }
                    else
                    {
                        Console.WriteLine(missingHerbsMessage);
                    }
                };
                kettle.Interaction = kettleInteraction;
                kettle.InteractionDescription = "Collect the last four herbs in order to finish the brewing process.";

                // kill monk for herbs
                // (escape bear)
                // trade sth with mary
            #endregion

            #region Set GameSteps and Player
                Game.CurrentGame.Player = player;
                Game.CurrentGame.GameProgressSteps = gameSteps;
            #endregion
        }
    }
}
