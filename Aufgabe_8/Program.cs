using System;

namespace Aufgabe_8
{
    class Program
    {
        public static char[] _GameData = {'1','2','3','4','5','6','7','8','9'};
        public static char _CurrentPlayer = 'O';

        static void Main(string[] args)
        {
            StartGame();
        }

        public static void StartGame()
        {
            string EndOfGameAnnouncement = "";

            Console.WriteLine($"────────────────────────");
            Console.WriteLine("Das Spiel beginnt!");
            Console.WriteLine("────────────────────────");

            for (;;)
            {
                PrintField();

                // Anweisung zur Eingabe
                Console.WriteLine($"Spieler {_CurrentPlayer}: Gib die Stelle an, an dem du dein Zeichen platzieren willst.");
                Console.Write("> ");
                string PlayerInput = Console.ReadLine();
                Console.WriteLine("────────────────────────");

                // Abbruchbedingung: Spieler bricht das Spiel ab => Abbruch
                if (PlayerInput.ToLower() == "exit")
                {
                    EndOfGameAnnouncement = "Au revoir!";
                    break;
                }

                // Eingabe verarbeiten
                ProcessPlayerInput(PlayerInput);
                
                // Abbruchbedingung: Spielfeld ist voll => Unentschieden
                if (IsFieldFull())
                {
                    EndOfGameAnnouncement = "Quel horreur... Das Spielfeld ist voll. Revanche?";
                    break;
                }

                // Abbruchbedingung: Spielfeld ist voll => Spieler gewinnt
                if (GetWinner() != '0')
                {
                    EndOfGameAnnouncement = $"Très bien Spieler {GetWinner()}. Du hast gewonnen!";
                    break;
                }

                // Spieler wechseln
                _CurrentPlayer = _CurrentPlayer == 'O' ? 'X' : 'O';
            }

            PrintField();
            Console.WriteLine("Spiel zu Ende.");
            Console.WriteLine(EndOfGameAnnouncement);
        }

        public static void ProcessPlayerInput(string PlayerInput)
        {
            if (PlayerInput.Length > 1)
            {
                _CurrentPlayer = _CurrentPlayer == 'O' ? 'X' : 'O';
                Console.WriteLine("Ungültige Eingabe.");
                Console.WriteLine("────────────────────────");
                return;
            }

            try
            {
                if (int.Parse(_GameData[int.Parse(PlayerInput) - 1]+"").GetType() == typeof(int))
                    _GameData[int.Parse(PlayerInput) - 1] = _CurrentPlayer;
                else
                {
                    _CurrentPlayer = _CurrentPlayer == 'O' ? 'X' : 'O';
                    Console.WriteLine("Die angegebene Stelle ist schon besetzt.");
                    Console.WriteLine("────────────────────────");
                }

            }
            catch (Exception)
            {
                _CurrentPlayer = _CurrentPlayer == 'O' ? 'X' : 'O';
                Console.WriteLine("Ungültige Eingabe.");
                Console.WriteLine("────────────────────────");
            }
        }

        public static char GetWinner()
        {
            int[,] CheckPatterns = new int[,] {{1,2,3},{4,5,6},{7,8,9},{1,4,7},{2,5,8},{3,6,9},{1,5,9},{3,5,7}};
            char Winner = '0';

            for (var i = 0; i < CheckPatterns.GetLength(0); i++)
            {
                if (_GameData[CheckPatterns[i,0] - 1] == _GameData[CheckPatterns[i,1] - 1] && _GameData[CheckPatterns[i,1] - 1] == _GameData[CheckPatterns[i,2] - 1])
                {
                    Winner = _GameData[CheckPatterns[i,0] - 1];
                }
            }

            return Winner;
        }

        public static bool IsFieldFull()
        {
            foreach (var GameDataEntry in _GameData)
            {
                try
                {
                    if (int.Parse(GameDataEntry+"").GetType() == typeof(int))
                    {
                        return false;
                    }
                }
                catch (Exception) {}
            }
            return true;
        }

        public static void PrintField()
        {
            Console.WriteLine($"| {_GameData[0]} | {_GameData[1]} | {_GameData[2]} |");
            Console.WriteLine($"| {_GameData[3]} | {_GameData[4]} | {_GameData[5]} |");
            Console.WriteLine($"| {_GameData[6]} | {_GameData[7]} | {_GameData[8]} |");
            Console.WriteLine($"────────────────────────");
        }
    }
}
