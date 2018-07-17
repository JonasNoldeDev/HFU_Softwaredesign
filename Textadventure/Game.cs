using System;

namespace Textadventure
{
    class Game
    {
        static void Main(string[] args)
        {
            Game game = new Game(GamePresets.Preset1);
        }

        public static Game CurrentGame;
        public Character Player;
        public int CurrentGameProgressStep = 0;
        private bool _gameOver = false;
        public GameStep[] GameProgressSteps;

        public Game(Action preset)
        {
            CurrentGame = this;
            InitializeGame(preset);
            StartGame();
        }

        private void UpdateGameProgress()
        {
            if (!_gameOver)
            {
                if (GameProgressSteps[CurrentGameProgressStep].NextStepCondition())
                {
                    CurrentGameProgressStep++;
                }
                if (Player.Health <= 0)
                {
                    CurrentGameProgressStep = -2; // -2 = game over
                    _gameOver = true;
                }
            }
            if (CurrentGameProgressStep == GameProgressSteps.Length)
            {
                CurrentGameProgressStep = -3; // -3 = victory
                _gameOver = true;
            }
            if (!_gameOver) Console.WriteLine("! Quest: " + GameProgressSteps[CurrentGameProgressStep].DescriptionForReachingNextStep);
        }

        private void ParsePlayerInput(String input)
        {
            String command = input.Split(' ')[0];
            String parameter = input.Split(' ').Length > 1 ? input.Split(' ')[1] : null;
    
            switch(command)
            {
                case "drop":
                    foreach (var item in Player.Inventory)
                    {
                        if (item.UniqueID == parameter)
                        {
                            Player.RemoveFromInventory(item);
                            Player.CurrentArea.Things.Add(item);
                            return;
                        }
                    }
                    Console.WriteLine($"Item with the ID [{parameter}] is not in your inventory.");
                    break;
                case "take":
                    foreach (var item in Player.CurrentArea.Things)
                    {
                        if (item is Item && item.UniqueID == parameter)
                        {
                            if (Player.Inventory.Count < Player.InventorySize)
                            {
                                Player.CurrentArea.Things.Remove(item);
                                Player.AddToInventory((Item)item);
                            }
                            else
                            {
                                Console.WriteLine("Your inventory is full.");
                            }
                            return;
                        }
                    }
                    Console.WriteLine($"Item with the ID [{parameter}] is not in the area.");
                    break;
                case "inventory":
                    if (Player.Inventory.Count == 0)
                    {
                        Console.WriteLine($"You have no items at the moment.");
                    }
                    else
                    {
                        Console.WriteLine($"Let's have a look at your belongings!");
                        foreach(var Item in Player.Inventory)
                        {
                            Helpers.WriteLine(Item.Name + $" [{Item.UniqueID}]", Item.Color);
                        }
                    }
                    break;
                case "attack":
                    foreach (var target in Player.CurrentArea.Things)
                    {
                        if (target.UniqueID == parameter)
                        {
                            if (target is Character)
                            {
                                Character character = (Character)target;
                                if (character.Attackable)
                                {
                                    character.Health -= Player.Attack;
                                    Player.Health -= character.Attack;
                                    if (character.Health <= 0) character.Die();
                                }
                                else
                                {
                                    Console.WriteLine($"You would really attack {character.Name}??");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Why would you even attack this?");
                            }
                            return;
                        }
                    }
                    Console.WriteLine("Your target doesn't seem to be here.");
                    break;
                case "explore":
                    Helpers.WriteLine("You can see:");

                    if ((Player.CurrentArea.Things.Count) == 1) Helpers.Write("nothing..."); // 1 because of the player character itself

                    foreach (var thing in Player.CurrentArea.Things)
                    {
                        if (thing is Item) Helpers.WriteLine(thing.Description + $" [{thing.UniqueID}]", Item.Color);
                        if (thing is Object) Helpers.WriteLine(thing.Description + $" [{thing.UniqueID}]", Object.Color);
                        if (thing is Character && thing != Game.CurrentGame.Player) Helpers.WriteLine(thing.Description + $" [{thing.UniqueID}]", Character.Color);
                    }

                    foreach (var areaDirection in Player.CurrentArea.Directions)
                    {
                        Helpers.WriteLine($"{areaDirection.Value.Name} towards [{areaDirection.Key}]", Area.Color);
                    }

                    break;
                case "inspect":
                    foreach (var thing in Player.CurrentArea.Things)
                    {
                        if (thing.UniqueID == parameter)
                        {
                            Console.WriteLine(thing.Description);
                            if (thing is Character)
                            {
                                Character character = (Character)thing;
                                Console.WriteLine($"Health: {character.Health}, Attack: {character.Attack}");
                            }
                            return;
                        }
                    }
                    Console.WriteLine($"Nothing with the ID [{parameter}] is in this area.");
                    break;
                case "move":
                    if (parameter != null && Player.CurrentArea.Directions.ContainsKey(parameter))
                    {
                        Player.MoveToArea(Player.CurrentArea.Directions[parameter]);
                        Console.WriteLine($"You're now entering '{Player.CurrentArea.Name}'.");
                    }
                    else
                    {
                        Console.WriteLine("You cannot move towards this direction.");
                    }
                    break;
                case "interact":
                    foreach (var thing in Player.CurrentArea.Things)
                    {
                        if (thing.UniqueID == parameter)
                        {
                            if (thing.Interaction != null)
                            {
                                thing.Interaction();
                            }
                            else
                            {
                                Console.WriteLine("You can't interact with that.");
                            }
                            return;
                        }
                    }
                    Console.WriteLine($"You can't see anything with the ID [{parameter}] here.");
                    break;
                case "help":
                    Console.WriteLine("Available commands: drop [ID], take [ID], inventory, attack [ID], explore, inspect [ID], move [north/east/south/west], interact [ID], quit");
                    Helpers.Write("Item [ID] ", Item.Color);
                    Helpers.Write("Object [ID] ", Object.Color);
                    Helpers.Write("Character [ID] ", Character.Color);
                    Helpers.Write("Area [ID] ", Area.Color);
                    Helpers.Write("\nExample: ");
                    Helpers.Write("Harry Potters Cloak [7]", Item.Color);
                    Helpers.Write(" - > take 7\n");
                    break;
                case "quit":
                    CurrentGameProgressStep = -1; // -1 = quit
                    _gameOver = true;
                    break;
                default:
                    Console.WriteLine($"Unknown command: '{command}'");
                    break;
            }
        }

        private void InitializeGame(Action preset)
        {
            preset();
        }

        // Main game loop
        private void StartGame()
        {
            Console.WriteLine("Alcimedes: Welcome to my home, my druid apprentice! Could you do me a favor?");
            Console.WriteLine("Type in 'help' to see all available commands!");

            while (!_gameOver)
            {
                // Get player input
                Console.Write("> ");
                string PlayerInput = Console.ReadLine();

                // Process player input
                ParsePlayerInput(PlayerInput);
                UpdateGameProgress();
            }

            EndGame();
        }

        private void EndGame()
        {
            switch(CurrentGameProgressStep)
            {
                case -1: // quit
                    Console.WriteLine("I'm sad you wan't to leave. Bye!");
                    break;
                case -2: // game over
                    Console.WriteLine("You lost! Ending narration...");
                    break;
                case -3: // victory
                    Console.WriteLine("You won! Ending narration...");
                    break;
                default:
                    break;
            }
        }
    }
}
