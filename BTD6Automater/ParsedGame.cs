﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace BTD6Automater
{
    public class ParsedScript : ScriptedGame
    {
        private BTD6GamePlayer _player;
        private MoneyReader _moneyReader;
        private string _name;
        private IEnumerable<string> _actions;

        private Dictionary<string, Tower> towers;
        private Dictionary<string, Point> setLocations;

        private Size _resolution;

        public ParsedScript(BTD6GamePlayer player, string file)
        {
            _player = player;
            var lines = File.ReadAllLines(file);
            _name = lines[0];
            var resolution = lines[1].Split(' ');
            _resolution = new Size(int.Parse(resolution[0]), int.Parse(resolution[1]));
            _moneyReader = new MoneyReader(_resolution.Width, _resolution.Height);
            _actions = lines.Skip(1);

            PrepareKeyWordDictionary();
        }
         
        public void DoActions()
        {
            towers = new Dictionary<string, Tower>();
            setLocations = new Dictionary<string, Point>();

            _player.Wait(1000);

            foreach (var action in _actions)
            {
                DoAction(action);
            }

            _player.Wait(1000);
        }

        public override string ToString()
        {
            return _name;
        }

        private void DoAction(string action)
        {
            var actionComponents = action.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (actionComponents.Length < 1)
            {
                return;
            }

            var keyword = actionComponents[0].ToLower();
            if (actionKeywords.ContainsKey(keyword))
            {
                actionKeywords[keyword](actionComponents);
            }
        }

        private void Wait(string[] args)
        {
            _player.Wait(int.Parse(args[1]));
        }

        private void WaitUntil(string[] args)
        {
            var desiredAmount = int.Parse(args[1]);
            var amount = 0;

            while (desiredAmount > amount)
            {
                _player.Wait(200);
                CollectBananas(args);
                amount = _moneyReader.ReadMoney(desiredAmount);
            }
            Console.WriteLine("Current Money: " + amount + " (desired: " + desiredAmount + ")");
        }

        private void StartRound(string[] args)
        {
            _player.StartRound();
        }

        private void SendRoundsInRace(string[] args)
        {
            var numberRounds = 1;
            if (args.Length > 1)
            {
                int.TryParse(args[1], out numberRounds);
            }

            var location = setLocations["send"];

            Console.WriteLine($"Sending {numberRounds} Rounds");

            for (var i = 0; i < numberRounds; i++)
            {
                _player.SendOneRoundInRace(location.X, location.Y);
            }

            _player.Wait(BTD6GamePlayer.MINIMUM_DELAY);
        }

        private void ChangeTargetting(string[] args)
        {
            var numberOFChanges = 1;
            if (args.Length > 1)
            {
                int.TryParse(args[2], out numberOFChanges);
            }

            var tower = towers[args[1]];

            _player.ChangeTargeting(tower, numberOFChanges);
        }

        private void ToggleFastForward(string[] args)
        {
            _player.ToggleFastForward();
        }

        private void GoFreePlay(string[] args)
        {
            _player.GoFreePlay(_freePlayButtonLocations[_resolution], _okButtonLocations[_resolution]);
        }

        private void RestartGame(string[] args)
        {
            _player.Restart(_restartButtonLocations[_resolution], _isSureButtonLocations[_resolution]);
        }

        private void PlaceTower(string[] args)
        {
            var towerType = GetTowerType(args[1]);
            var name = args[4];
            var posX = int.Parse(args[2]);
            var posY = int.Parse(args[3]);

            var tower = _player.PlaceTower(towerType, posX, posY, name);

            towers.Add(name, tower);
        }

        private void UpgradeTower(string[] args)
        {
            var num = 1;
            if (args.Length > 3)
            {
                num = int.Parse(args[3]);
            }

            _player.UpgradeTower(towers[args[1]], GetPath(args[2]), num);
        }

        private void SellTower(string[] args)
        {
            _player.SellTower(towers[args[1]]);
            towers.Remove(args[1]);
        }

        private void SetLocation(string[] args)
        {
            var name = args[1].ToLower();
            var x = int.Parse(args[2]);
            var y = int.Parse(args[3]);

            setLocations.Add(name, new Point(x, y));
        }

        private void CollectBananas(string[] args)
        {
            foreach(var tower in towers.Values)
            {
                if (tower.TowerType == TowerType.Farm)
                {
                    _player.CollectBananas(tower);
                }
            }
        }

        private void PrepareKeyWordDictionary()
        {
            actionKeywords.Add("wait", Wait);
            actionKeywords.Add("waituntil", WaitUntil);
            actionKeywords.Add("start", StartRound);
            actionKeywords.Add("send", SendRoundsInRace);
            actionKeywords.Add("ff", ToggleFastForward);
            actionKeywords.Add("place", PlaceTower);
            actionKeywords.Add("upgrade", UpgradeTower);
            actionKeywords.Add("sell", SellTower);
            actionKeywords.Add("freeplay", GoFreePlay);
            actionKeywords.Add("restart", RestartGame);
            actionKeywords.Add("collect", CollectBananas);
            actionKeywords.Add("target", ChangeTargetting);

            actionKeywords.Add("set", SetLocation);
        }

        private Dictionary<string, Action<string[]>> actionKeywords = new Dictionary<string, Action<string[]>>();

        private TowerType GetTowerType(string name)
        {
            return (TowerType) Enum.Parse(typeof(TowerType), name, true);
        }

        private UpgradePath GetPath(string name)
        {
            return (UpgradePath)Enum.Parse(typeof(UpgradePath), name, true);
        }

        private Dictionary<Size, Point> _freePlayButtonLocations = new Dictionary<Size, Point>()
        {
            { new Size(1024, 768), new Point(820, 650) },
            { new Size(1920, 1080), new Point(1150, 900) }
        };

        private Dictionary<Size, Point> _okButtonLocations = new Dictionary<Size, Point>()
        {
            { new Size(1024, 768), new Point(660, 540) },
            { new Size(1920, 1080), new Point(960, 760) }
        };

        private Dictionary<Size, Point> _restartButtonLocations = new Dictionary<Size, Point>()
        {
            { new Size(1024, 768), new Point(780, 640) },
            { new Size(1920, 1080), new Point(1090, 900) }
        };

        private Dictionary<Size, Point> _isSureButtonLocations = new Dictionary<Size, Point>()
        {
            { new Size(1024, 768), new Point(830, 530) },
            { new Size(1920, 1080), new Point(1175, 760) }
        };
    }
}
