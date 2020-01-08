using BTD6Automater;
using System;
using System.Collections.Generic;

namespace GameAutomater
{
    class Program
    {
        static WindowInteractions winInteractions = new WindowFormInteractions();
        static GamePlayer btd6Player = new GamePlayer(winInteractions);
        static ScriptLoader scriptLoader = new ScriptLoader();

        static Dictionary<string, ScriptedGame> scripts = new Dictionary<string, ScriptedGame>();

        static void Main(string[] args)
        {
            var loadedScripts = scriptLoader.LoadScripts(btd6Player, @"..\..\..\Strategies\", ".btd6");
            foreach (var script in loadedScripts)
            {
                scripts.Add((scripts.Count + 1).ToString(), script);
            }

            var choice = "s";

            while (choice != "q")
            {
                PrintOptionsMenu();

                choice = Console.ReadLine();
                Console.WriteLine();
                ExecuteChosenOption(choice);
            }
        }

        private static void ExecuteChosenOption(string choice)
        {
            if (choice == "m")
            {
                PrintCursorCoordinates();
            }
            else if (scripts.ContainsKey(choice))
            {
                ExecuteSelectedStrategy(choice);
            }
        }

        private static void ExecuteSelectedStrategy(string choice)
        {
            int loops = AskForNumberOfLoops();
            ExecuteStrategy(scripts[choice], loops);
        }

        private static void PrintOptionsMenu()
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("\tQ: Exit");
            Console.WriteLine("\tM: Print your current cursor coordinates");

            foreach (var strat in scripts)
            {
                Console.WriteLine("\t" + strat.Key + ": " + strat.ToString());
            }
        }

        private static void ExecuteStrategy(ScriptedGame strategy, int loops)
        {
            winInteractions.MinimizeCurrentWindow();

            var loopNum = 0;

            while (loopNum < loops || loops == 0)
            {
                ExecuteStrategy(strategy);
            }

            winInteractions.MaximizeCurrentWindow();
        }

        private static void ExecuteStrategy(ScriptedGame strategy)
        {
            strategy.DoActions();
        }

        private static int AskForNumberOfLoops()
        {
            Console.WriteLine("How many loops? (0 for infinite)");
            var loops = int.Parse(Console.ReadLine());
            return loops;
        }

        private static void PrintCursorCoordinates()
        {
            var coordinates = winInteractions.GetCursorLocation();
            Console.WriteLine("[" + coordinates.X + ", " + coordinates.Y + "]");
        }
    }
}
