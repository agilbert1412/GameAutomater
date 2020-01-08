using System;
using System.Collections.Generic;
using System.Threading;

namespace BTD6Automater
{
    public class GamePlayer
    {
        private const int MINIMUM_DELAY = 200;
        private const int BUTTON_DELAY = 2000;

        private WindowInteractions _gameWindow;

        public GamePlayer(WindowInteractions interactor)
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

        public Tower PlaceTower(TowerType tower, int locationX, int locationY)
        {
            Console.WriteLine("Placing tower " + tower + " at location [" + locationX + ", " + locationY + "]");

            SelectTower(tower);
            Wait(MINIMUM_DELAY);
            _gameWindow.SendClick(locationX, locationY);

            return new Tower(locationX, locationY);
        }

        public void UpgradeTower(Tower tower, UpgradePath path)
        {
            _gameWindow.SendClick(tower.X, tower.Y);
            Wait(MINIMUM_DELAY);
            _gameWindow.SendKey(GetHotkey(path));
            Wait(MINIMUM_DELAY);
            _gameWindow.SendKey("{ESC}");
        }

        public void Restart()
        {
            _gameWindow.SendKey("{ESC}");
            Wait(BUTTON_DELAY);
            _gameWindow.SendClick(780, 640);
            Wait(BUTTON_DELAY);
            _gameWindow.SendClick(830, 530);
        }

        public void GoFreePlay()
        {
            _gameWindow.SendClick(820, 650);
            Wait(BUTTON_DELAY);
            _gameWindow.SendClick(660, 540);
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
