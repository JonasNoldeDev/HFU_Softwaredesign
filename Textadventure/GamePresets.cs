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
                    Description = "Ahh! So this is Alcimedes' Shack."
                };
                Area colorfulFields = new Area()
                {
                    Name = "The Colorful Fields",
                    Description = "Wow! These are colorful fields."
                };
                Area deepCaves = new Area()
                {
                    Name = "The Deep Caves",
                    Description = "Ahh! So these are the Deep Caves"
                };
                Area infiniteWoodlands = new Area()
                {
                    Name = "The Infinite Woodlands",
                    Description = "Ahh! So these are the Infinite Woodlands"
                };
                Area monastery = new Area()
                {
                    Name = "The Monastery",
                    Description = "Ahh! So that's the Monastery"
                };

                alcimedesShack.Directions.Add("north", infiniteWoodlands);
                alcimedesShack.Directions.Add("west", colorfulFields);
                colorfulFields.Directions.Add("north", monastery);
                colorfulFields.Directions.Add("east", alcimedesShack);
                deepCaves.Directions.Add("west", infiniteWoodlands);
                infiniteWoodlands.Directions.Add("east", deepCaves);
                infiniteWoodlands.Directions.Add("south", alcimedesShack);
                infiniteWoodlands.Directions.Add("west", monastery);
                monastery.Directions.Add("east", infiniteWoodlands);
                monastery.Directions.Add("south", colorfulFields);
            #endregion

            #region Initialize Items
                Item yellowHerb = new Item()
                {
                    Name = "Yellow Herb",
                    Description = "Yellow Herb"
                };
                Item blueHerb = new Item()
                {
                    Name = "Blue Herb",
                    Description = "Blue Herb"
                };
                Item redHerb = new Item()
                {
                    Name = "Red Herb",
                    Description = "Red Herb"
                };
                Item whiteHerb = new Item()
                {
                    Name = "White Herb",
                    Description = "White Herb"
                };
                Item magicRod = new Item()
                {
                    Name = "Magic Rod",
                    Description = "Magic Rod"
                };
                Item medicine = new Item()
                {
                    Name = "Herbal Medicine",
                    Description = "Herbal Medicine"
                };
            #endregion

            #region Initialize Objects
                Object kettle = new Object()
                {
                    Name = "Kettle",
                    Description = "A huge kettle"
                };
            #endregion

            #region Initialize Characters
                Character player = new Character()
                {
                    Name = "Player",
                    Description = "A young druid's apprentice",
                    InventorySize = 10,
                    Health = 10,
                    Attack = 10
                };
                Character alcimedes = new Character()
                {
                    Name = "Alcimedes",
                    Description = "Alcimedes, an old druid and also your teacher",
                    InventorySize = 10,
                    Health = 5,
                    Attack = 0
                };
                Character mary = new Character()
                {
                    Name = "Mary",
                    Description = "A beautiful flower maiden with curly blonde hair",
                    InventorySize = 10,
                    Health = 10,
                    Attack = 0
                };
                Character bear = new Character()
                {
                    Name = "The grizzly",
                    Description = "A big gray grizzly bear",
                    InventorySize = 10,
                    Health = 15,
                    Attack = 3,
                    Attackable = true
                };
                Character monk = new Character()
                {
                    Name = "Mohammed Lee",
                    Description = "A misterious monk",
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
                    new GameStep( // 0
                        () => Game.CurrentGame.Player.Inventory.Contains(magicRod),
                        "Talk to Alcimedes"
                    ),
                    new GameStep( // 1
                        () => {
                            foreach (var herb in new Item[]{redHerb, yellowHerb, whiteHerb, blueHerb})
                            {
                                if (!Game.CurrentGame.Player.Inventory.Contains(herb))
                                {
                                    return false;
                                }
                            }
                            return true;
                        },
                        "Collect the four herbs"
                    ),
                    new GameStep( // 2
                        () => Game.CurrentGame.Player.Inventory.Contains(medicine),
                        "Brew the medicine in Alcimedes' kettle"
                    ),
                    new GameStep( // 3
                        () => alcimedes.Inventory.Contains(medicine),
                        "Bring the medicine to Alcimedes"
                    )
                    // get fire wood ?
                    // get all herbs
                    // brew medicine
                };
            #endregion
            
            #region Add Interactions
                kettle.Interaction = () =>
                {
                    if (Game.CurrentGame.CurrentGameProgressStep <= 2)
                    {
                        bool allHerbsAvailable = true;
                        Item[] requiredHerbs = new Item[]{ yellowHerb, blueHerb, redHerb, whiteHerb };
                        String missingHerbsMessage  = "Collect the last four herbs to finish brewing the medicine.\nThe following herbs are missing in your inventory:\n";
                        foreach (var herb in requiredHerbs)
                        {
                            if (!Game.CurrentGame.Player.Inventory.Contains(herb))
                            {
                                allHerbsAvailable = false;
                                missingHerbsMessage += herb.Name + "\n";
                            }
                        }
                        missingHerbsMessage += "You have to find them and come back here again.";
                        if (allHerbsAvailable)
                        {
                            foreach (var herb in requiredHerbs)
                            {
                                Game.CurrentGame.Player.Inventory.Remove(herb);
                            }
                            Game.CurrentGame.Player.Inventory.Add(medicine);
                            Console.WriteLine("All the herbs are gone.. But finally you have the medicine for Alcimedes!");
                        }
                        else
                        {
                            Console.WriteLine(missingHerbsMessage);
                        }
                    }
                    else
                    {
                        Console.WriteLine("The kettle is completely emty.");
                    }
                };

                alcimedes.Interaction = () =>
                {
                    switch (Game.CurrentGame.CurrentGameProgressStep)
                    {
                        case 0:
                            Console.WriteLine("Alcimedes: Hello again. Here's a rod. I don't need it anyway. Well... Nobody does.");
                            Game.CurrentGame.Player.AddToInventory(magicRod);
                            Console.WriteLine("Alcimedes: In exchange I want you to finish brewing my herbal medicine. It's brewed out of 56 herbs - really delicious!");
                            Console.WriteLine("Alcimedes: I just need the last four herbs but that's no problem for you, right?");
                            break;
                        case 3:
                            Game.CurrentGame.Player.RemoveFromInventory(medicine);
                            alcimedes.AddToInventory(medicine);
                            Console.WriteLine("Alcimedes: Oh! Perfect! There's my beloved Jägerm-m-mm-medicine... Thank you!");
                            break;
                        default:
                            Console.WriteLine("Alcimedes: What a nice day for a bourbon, huh?");
                            break;
                    }
                };

                bear.Interaction = () =>
                {
                    Console.WriteLine("Grrrrr!!");
                };

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
