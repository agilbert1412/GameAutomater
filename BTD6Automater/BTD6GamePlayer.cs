using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Interactions;

namespace BTD6Automater
{
    public class BTD6GamePlayer
    {
        private const int SEND_ROUNDS_DELAY = 50;
        private const int FARM_DELTA = 50;
        public const int MINIMUM_DELAY = 120;
        private const int BUTTON_DELAY = 2000;

        private WindowInteractions _gameWindow;

        public BTD6GamePlayer(WindowInteractions interactor)
        {
            _gameWindow = interactor;
        }

        public void StartRound()
        {
            Console.WriteLine("Starting the round");

            PressSpace();
        }

        public void ToggleFastForward()
        {
            PressSpace();
        }

        public void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        public Tower PlaceTower(TowerType tower, int locationX, int locationY, string name = "")
        {
            Console.WriteLine("Placing tower " + tower + " at location [" + locationX + ", " + locationY + "]");

            SelectTower(tower);
            Wait(MINIMUM_DELAY);
            _gameWindow.SendClick(locationX, locationY);
            Wait(MINIMUM_DELAY);

            return new Tower(tower, name, locationX, locationY);
        }

        public void UpgradeTower(Tower tower, UpgradePath path, int numUpgrades = 1)
        {
            Console.WriteLine($"Upgrading tower {tower.Name} on {path.ToString()} path");

            _gameWindow.SendClick(tower.X, tower.Y);
            Wait(MINIMUM_DELAY);
            for (var i = 0; i < numUpgrades; i++)
            {
                _gameWindow.SendKey(GetHotkey(path));
                Wait(MINIMUM_DELAY);
            }
            _gameWindow.SendKey("{ESC}");
            Wait(MINIMUM_DELAY);
        }

        public void SellTower(Tower tower)
        {
            Console.WriteLine($"Selling tower {tower.Name}");

            _gameWindow.SendClick(tower.X, tower.Y);
            Wait(MINIMUM_DELAY);
            _gameWindow.SendKey("{BACKSPACE}");
            Wait(MINIMUM_DELAY);
            Wait(MINIMUM_DELAY);
        }

        public void Restart(Point restartButtonLocation, Point isSureButtonLocation)
        {
            _gameWindow.SendKey("{ESC}");
            Wait(BUTTON_DELAY);
            _gameWindow.SendClick(restartButtonLocation.X, restartButtonLocation.Y);
            Wait(BUTTON_DELAY);
            _gameWindow.SendClick(isSureButtonLocation.X, isSureButtonLocation.Y);
            Wait(MINIMUM_DELAY);
        }

        public void GoFreePlay(Point freePlayButtonLocation, Point okButtonLocation)
        {
            _gameWindow.SendClick(freePlayButtonLocation.X, freePlayButtonLocation.Y);
            Wait(BUTTON_DELAY);
            _gameWindow.SendClick(okButtonLocation.X, okButtonLocation.Y);
            Wait(MINIMUM_DELAY);
        }

        public void CollectBananas(Tower farm)
        {
            _gameWindow.PlaceCursor(farm.X - FARM_DELTA, farm.Y - FARM_DELTA);
            Wait(SEND_ROUNDS_DELAY);
            _gameWindow.PlaceCursor(farm.X + FARM_DELTA, farm.Y - FARM_DELTA);
            Wait(SEND_ROUNDS_DELAY);
            _gameWindow.PlaceCursor(farm.X - FARM_DELTA, farm.Y + FARM_DELTA);
            Wait(SEND_ROUNDS_DELAY);
            _gameWindow.PlaceCursor(farm.X + FARM_DELTA, farm.Y + FARM_DELTA);
            Wait(SEND_ROUNDS_DELAY);
        }

        public void SendOneRoundInRace(int x, int y)
        {
            Wait(SEND_ROUNDS_DELAY);
            _gameWindow.SendClick(x, y);
        }

        public void ChangeTargeting(Tower tower, int numChanges)
        {            
            _gameWindow.SendClick(tower.X, tower.Y);
            Wait(MINIMUM_DELAY);

            for (var i = 0; i < numChanges; i++)
            {
                _gameWindow.SendKey("{TAB}");
                Wait(SEND_ROUNDS_DELAY);
            }

            _gameWindow.SendKey("{ESC}");
            Wait(MINIMUM_DELAY);
        }

        private void PressSpace()
        {
            _gameWindow.SendKey(" ");
            Wait(MINIMUM_DELAY);
        }

        private void SelectTower(TowerType tower)
        {
            _gameWindow.SendKey(GetHotkey(tower));
        }

        private void SelectGameWindow()
        {
            _gameWindow.Focus("BloonsTD6");
        }

        private string GetHotkey(TowerType tower)
        {
            return towerHotkeys[tower];
        }

        private string GetHotkey(UpgradePath upgradePath)
        {
            return upgradeHotkeys[upgradePath];
        }

        private Dictionary<TowerType, string> towerHotkeys = new Dictionary<TowerType, string>
        {
            { TowerType.Hero, "u" },
            { TowerType.Dart, "q" },
            { TowerType.Boomerang, "w" },
            { TowerType.Bomb, "e" },
            { TowerType.Tack, "r" },
            { TowerType.Ice, "t" },
            { TowerType.Glue, "y" },
            { TowerType.Sniper, "z" },
            { TowerType.Submarine, "x" },
            { TowerType.Boat, "c" },
            { TowerType.Ace, "v" },
            { TowerType.Heli, "b" },
            { TowerType.Mortar, "n" },
            { TowerType.Wizard, "a" },
            { TowerType.Super, "s" },
            { TowerType.Ninja, "d" },
            { TowerType.Alchemist, "f" },
            { TowerType.Druid, "g" },
            { TowerType.Farm, "h" },
            { TowerType.SpikeFactory, "j" },
            { TowerType.Village, "k" },
            { TowerType.Engineer, "l" }
        };

        private Dictionary<UpgradePath, string> upgradeHotkeys = new Dictionary<UpgradePath, string>
        {
            { UpgradePath.Top, "," },
            { UpgradePath.Middle, "." },
            { UpgradePath.Bottom, "é" },
        };
    }
}
