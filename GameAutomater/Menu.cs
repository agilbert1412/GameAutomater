using BTD6Automater;
using System;
using System.Collections.Generic;

namespace GameAutomater
{
    public class Menu
    {
        private const string COMMAND_START = "S";
        private const string COMMAND_EXIT = "Q";
        private const string COMMAND_PRINT_CURSOR_LOCATION = "M";

        private WindowInteractions _winInteractions;
        private GamePlayer _gamePlayer;
        private ScriptExecuter _scriptExecuter;

        private Dictionary<string, ScriptedGame> _scripts;

        internal Menu(WindowInteractions winInteractions, GamePlayer player, ScriptLoader scriptLoader, ScriptExecuter executer)
        {
            _winInteractions = winInteractions;
            _gamePlayer = player;
            _scriptExecuter = executer;
            LoadScripts(scriptLoader);
        }

        internal void Run()
        {
            var choice = COMMAND_START;

            while (choice.ToUpper() != COMMAND_EXIT)
            {
                PrintOptionsMenu();

                choice = Console.ReadLine();
                Console.WriteLine();
                ExecuteChosenOption(choice);
            }
        }

        private void LoadScripts(ScriptLoader scriptLoader)
        {
            _scripts = new Dictionary<string, ScriptedGame>();
            var loadedScripts = scriptLoader.LoadScripts(_gamePlayer, ".btd6");
            foreach (var script in loadedScripts)
            {
                _scripts.Add((_scripts.Count + 1).ToString(), script);
            }
        }

        private void ExecuteChosenOption(string choice)
        {
            if (choice.ToUpper() == COMMAND_PRINT_CURSOR_LOCATION)
            {
                PrintCursorCoordinates();
            }
            else if (_scripts.ContainsKey(choice))
            {
                ExecuteSelectedScript(choice);
            }
        }

        private void ExecuteSelectedScript(string choice)
        {
            int loops = AskForNumberOfLoops();

            _winInteractions.MinimizeCurrentWindow();

            _scriptExecuter.ExecuteScript(_scripts[choice], loops);

            _winInteractions.MaximizeCurrentWindow();
        }

        private void PrintOptionsMenu()
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine($"\t{COMMAND_EXIT}: Exit");
            Console.WriteLine($"\t{COMMAND_PRINT_CURSOR_LOCATION}: Print your current cursor coordinates");

            foreach (var script in _scripts)
            {
                Console.WriteLine("\t" + script.Key + ": " + script.ToString());
            }
        }

        private int AskForNumberOfLoops()
        {
            Console.WriteLine("How many loops? (0 for infinite)");
            var loops = int.Parse(Console.ReadLine());
            return loops;
        }

        private void PrintCursorCoordinates()
        {
            var coordinates = _winInteractions.GetCursorLocation();
            Console.WriteLine("[" + coordinates.X + ", " + coordinates.Y + "]");
        }
    }
}
