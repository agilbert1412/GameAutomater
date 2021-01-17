using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameAutomater;
using Interactions;

namespace BTD6Automater
{
    public class BTD6Menu : IGameMenu
    {
        private const string COMMAND_START = "S";
        private const string COMMAND_EXIT = "Q";
        private const string COMMAND_PRINT_CURSOR_LOCATION = "M";
        private const string COMMAND_TAKE_MONEY_PIC = "N";

        private WindowInteractions _winInteractions;
        private BTD6GamePlayer _btd6GamePlayer;
        private MoneyReader _moneyReader;

        private Dictionary<string, ScriptedGame> _scripts;
        
        static ScriptLoader _scriptLoader = new ScriptLoader();
        static ScriptExecuter _scriptExecuter = new ScriptExecuter();

        public BTD6Menu(WindowInteractions winInteractions)
        {
            _winInteractions = winInteractions;
            _btd6GamePlayer = new BTD6GamePlayer(winInteractions);
            LoadScripts(_scriptLoader);
            _moneyReader = new MoneyReader(1920, 1080);
        }

        public void Run()
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
            var loadedScripts = scriptLoader.LoadScripts(_btd6GamePlayer, ".btd6");
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
            else if (choice.ToUpper()[0] == COMMAND_TAKE_MONEY_PIC[0])
            {
                TakeMoneyPic(choice.Split(' ')[1]);
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
            Console.WriteLine($"\t{COMMAND_TAKE_MONEY_PIC}: Take a picture of your current money");

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

        private void TakeMoneyPic(string amountInName)
        {
            _moneyReader.TakeMoneyPic(amountInName);
        }
    }
}
