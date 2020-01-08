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
        static ScriptExecuter scriptExecuter = new ScriptExecuter();

        static Menu mainMenu;

        static void Main(string[] args)
        {
            mainMenu = new Menu(winInteractions, btd6Player, scriptLoader, scriptExecuter);
            mainMenu.Run();

            Console.WriteLine("Execution over, press any key to continue...");
            Console.ReadKey();
        }
    }
}
