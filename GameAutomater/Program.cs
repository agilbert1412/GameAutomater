using BTD6Automater;
using System;
using System.Collections.Generic;
using Interactions;

namespace GameAutomater
{
    class Program
    {
        static WindowInteractions winInteractions = new WindowFormInteractions();

        static GameMenu mainMenu;

        static void Main(string[] args)
        {
            mainMenu = new GameMenu(winInteractions);
            mainMenu.Run();

            Console.WriteLine("Execution over, press any key to continue...");
            Console.ReadKey();
        }
    }
}
