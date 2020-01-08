using BTD6Automater;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace GameAutomater
{
    class Program
    {
        static WindowInteractions winInteractions = new WindowFormInteractions();
        static GamePlayer btd6Player = new GamePlayer(winInteractions);

        static Dictionary<char, ScriptedGame> strategies = new Dictionary<char, ScriptedGame>
        {
            {'1', new MuddyPuddlesEasy(btd6Player) }
        };

        static void Main(string[] args)
        {
            LoadStrategies();

            var key = 's';

            while (key != 'q')
            {
                PrintOptionsMenu();

                key = Console.ReadKey().KeyChar;
                Console.WriteLine();
                ExecuteChosenOption(key);
            }
        }

        private static void LoadStrategies()
        {
            var files = Directory.EnumerateFiles(@"..\..\..\Strategies\", "*.btd6", SearchOption.AllDirectories);
            foreach (var strategyFile in files)
            {
                strategies.Add((strategies.Count + 1).ToString()[0], new ParsedGame(btd6Player, strategyFile));
            }
        }

        private static void ExecuteChosenOption(char key)
        {
            if (key == 'm')
            {
                PrintCursorCoordinates();
            }
            else if (strategies.ContainsKey(key))
            {
                ExecuteSelectedStrategy(key);
            }
        }

        private static void ExecuteSelectedStrategy(char key)
        {
            int loops = AskForNumberOfLoops();
            ExecuteStrategy(strategies[key], loops);
        }

        private static void PrintOptionsMenu()
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("\tQ: Exit");
            Console.WriteLine("\tM: Print your current cursor coordinates");

            foreach (var strat in strategies)
            {
                Console.WriteLine("\t" + strat.Key + ": " + strat.ToString());
            }
        }

        private static void ExecuteStrategy(ScriptedGame strategy, int loops)
        {
            MinimizeCurrentWindow();

            var loopNum = 0;

            while (loopNum < loops || loops == 0)
            {
                ExecuteStrategy(strategy);
            }

            MaximizeCurrentWindow();
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

        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow([In] IntPtr hWnd, [In] int nCmdShow);

        private static void MinimizeCurrentWindow()
        {
            IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;

            ShowWindow(handle, 6);
        }

        private static void MaximizeCurrentWindow()
        {
            IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;

            ShowWindow(handle, 9);
        }
    }
}
