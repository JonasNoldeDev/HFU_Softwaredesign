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
        private int CurrentGameProgressStep = 0;
        private bool GameOver = false;
        public GameStep[] GameProgressSteps;

        public Game(Action preset)
        {
            CurrentGame = this;
            InitializeGame(preset);
            StartGame();
        }

        private void UpdateGameProgress()
        {
            if (!GameOver && GameProgressSteps[CurrentGameProgressStep].NextStepCondition)
            {
                CurrentGameProgressStep++;
                Console.WriteLine(GameProgressSteps[CurrentGameProgressStep].DescriptionForReachingNextStep);
            }
            if (!GameOver && Player.Health <= 0)
            {
                CurrentGameProgressStep = -2; // -2 = game over
                GameOver = true;
            }
            if (CurrentGameProgressStep == GameProgressSteps.Length)
            {
                CurrentGameProgressStep = -3; // -3 = victory
                GameOver = true;
            }
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
                        if (item.Name == parameter)
                        {
                            Player.RemoveFromInventory(item);
                            Player.CurrentArea.Things.Add(item);
                            Console.WriteLine($"You dropped {item.Name} out of your inventory!");
                            break;
                        }
                    }
                    Console.WriteLine($"'{parameter}' is not in the inventory.");
                    break;
                case "take":
                    foreach (var item in Player.CurrentArea.Things)
                    {
                        if (item is Item && item.Name == parameter)
                        {
                            if (Player.Inventory.Count < Player.InventorySize)
                            {
                                Player.CurrentArea.Things.Remove(item);
                                Player.AddToInventory((Item)item);
                                Console.WriteLine($"{item.Name} has been added to your inventory!");
                            }
                            else
                            {
                                Console.WriteLine("The inventory is full.");
                            }
                            break;
                        }
                    }
                    Console.WriteLine($"'{parameter}' is not in the area.");
                    break;
                case "inventory":
                    if (Player.Inventory.Count == 0)
                    {
                        Console.WriteLine($"I have no items at the moment.");
                    }
                    else
                    {
                        Console.WriteLine($"Let's have a look at my belongings!");
                        foreach(var Item in Player.Inventory)
                        {
                            Helpers.WriteLine(Item.Name, Item.Color);
                        }
                    }
                    break;
                case "attack":
                    foreach (var target in Player.CurrentArea.Things)
                    {
                        if (target.Name == parameter)
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
                                    Console.WriteLine($"I'd never attack {character.Name}!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Why would you even attack this?");
                            }
                            break;
                        }
                    }
                    Console.WriteLine("My target doesn't seem to be here.");
                    break;
                case "explore":
                    Helpers.WriteLine("I can see:");

                    if ((Player.CurrentArea.Things.Count) == 1) Helpers.Write("nothing..."); // 1 because of the player character itself

                    foreach (var thing in Player.CurrentArea.Things)
                    {
                        if (thing is Item) Helpers.WriteLine(thing.Description, Item.Color);
                        if (thing is Object) Helpers.WriteLine(thing.Description, Object.Color);
                        if (thing is Character && thing != Game.CurrentGame.Player) Helpers.WriteLine(thing.Description, Character.Color);
                    }

                    break;
                case "inspect":
                    foreach (var thing in Player.CurrentArea.Things)
                    {
                        if (thing.Name == parameter)
                        {
                            Console.WriteLine(thing.Description);

                            if (thing is Character)
                            {
                                Character character = (Character)thing;
                                Console.WriteLine($"Health: {character.Health}, Attack: {character.Attack}");
                            }

                            break;
                        }
                    }
                    Console.WriteLine($"'{parameter}' is not in this area.");
                    break;
                case "move":
                    int directionToMove = 0;

                    switch(parameter)
                    {
                        case "north":
                            directionToMove = 0;
                            break;
                        case "east":
                            directionToMove = 1;
                            break;
                        case "south":
                            directionToMove = 2;
                            break;
                        case "west":
                            directionToMove = 3;
                            break;
                        case null:
                            Console.WriteLine("Where are we moving?");
                            break;
                        default:
                            Console.WriteLine("That's not even a direction to move.");
                            break;
                    }

                    if (Player.CurrentArea.Directions[directionToMove] != null)
                    {
                        Player.MoveToArea(Player.CurrentArea.Directions[directionToMove]);
                        Console.WriteLine($"You're now entering {Player.CurrentArea.Directions[directionToMove].Name}.");
                    }
                    else
                    {
                        Console.WriteLine("You cannot move in this direction.");
                    }
                    break;
                case "interact":
                    foreach (var thing in Player.CurrentArea.Things)
                    {
                        if (thing.Name == parameter)
                        {
                            if (thing.Interaction != null)
                            {
                                thing.Interaction();
                            }
                            else
                            {
                                Console.WriteLine("You can't interact with that.");
                            }
                            break;
                        }
                    }
                    Console.WriteLine($"I can't find '{parameter}'.");
                    break;
                case "quit":
                    CurrentGameProgressStep = -1; // -1 = quit
                    GameOver = true;
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
            Console.Write("Beginning narration.");

            while (!GameOver)
            {
                // Show player instructions and commands
                Console.WriteLine("Available commands: ...");

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
                    Console.WriteLine("Ending narration...");
                    break;
                case -3: // victory
                    Console.WriteLine("Ending narration...");
                    break;
                default:
                    break;
            }
        }
    }
}
