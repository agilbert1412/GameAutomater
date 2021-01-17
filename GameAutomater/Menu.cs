using BTD6Automater;
using System;
using System.Collections.Generic;
using Interactions;

namespace GameAutomater
{
    public class GameMenu
    {
        private Dictionary<string, IGameMenu> _availableGames;
        private const string COMMAND_EXIT = "Q";

        public GameMenu(WindowInteractions winInteractions)
        {
            _availableGames = new Dictionary<string, IGameMenu>();
            _availableGames.Add("1", new BTD6Menu(winInteractions));
            _availableGames.Add("2", new NertsMenu(winInteractions));
        }

        internal void Run()
        {
            var choice = "";

            while (choice.ToUpper() != COMMAND_EXIT)
            {
                PrintGamesMenu();

                choice = Console.ReadLine();
                Console.WriteLine();
                if (_availableGames.ContainsKey(choice))
                {
                    _availableGames[choice].Run();
                }
            }
        }

        private void PrintGamesMenu()
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("Q: Exit");

            foreach (var script in _availableGames)
            {
                Console.WriteLine("\t" + script.Key + ": " + script.ToString());
            }
        }
    }
}
