using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTD6Automater
{
    public class ParsedScript : ScriptedGame
    {
        private GamePlayer _player;
        string _name;
        IEnumerable<string> _actions;

        private Dictionary<string, Tower> towers;

        public ParsedScript(GamePlayer player, string file)
        {
            _player = player;
            var lines = File.ReadAllLines(file);
            _name = lines[0];
            _actions = lines.Skip(1);

            PrepareKeyWordDictionary();
        }

        private void DoAction(string action)
        {
            var actionComponents = action.Trim().Split(new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (actionComponents.Count() < 1)
            {
                return;
            }
            
            var keyword = actionComponents[0].ToLower();
            if (actionKeywords.ContainsKey(keyword))
            {
                actionKeywords[keyword](actionComponents);
            }
        }
         
        public void DoActions()
        {
            towers = new Dictionary<string, Tower>();

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

        private void Wait(string[] args)
        {
            _player.Wait(int.Parse(args[1]));
        }

        private void StartRound(string[] args)
        {
            _player.StartRound();
        }

        private void ToggleFastForward(string[] args)
        {
            _player.ToggleFastForward();
        }

        private void GoFreePlay(string[] args)
        {
            _player.GoFreePlay();
        }

        private void RestartGame(string[] args)
        {
            _player.Restart();
        }

        private void PlaceTower(string[] args)
        {
            var towerType = GetTowerType(args[1]);
            var posX = int.Parse(args[2]);
            var posY = int.Parse(args[3]);

            var tower = _player.PlaceTower(towerType, posX, posY);

            towers.Add(args[4], tower);
        }

        private void UpgradeTower(string[] args)
        {
            _player.UpgradeTower(towers[args[1]], GetPath(args[2]));
        }

        private void PrepareKeyWordDictionary()
        {
            actionKeywords.Add("wait", Wait);
            actionKeywords.Add("start", StartRound);
            actionKeywords.Add("ff", ToggleFastForward);
            actionKeywords.Add("place", PlaceTower);
            actionKeywords.Add("upgrade", UpgradeTower);
            actionKeywords.Add("freeplay", GoFreePlay);
            actionKeywords.Add("restart", RestartGame);
        }

        private Dictionary<string, Action<string[]>> actionKeywords = new Dictionary<string, Action<string[]>>();

        private TowerType GetTowerType(string name)
        {
            return (TowerType) Enum.Parse(typeof(TowerType), name, true);
        }

        private Path GetPath(string name)
        {
            return (Path)Enum.Parse(typeof(Path), name, true);
        }
    }
}
