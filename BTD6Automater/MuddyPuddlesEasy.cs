using System;

namespace BTD6Automater
{
    [Obsolete("All scripted games should be used through the parser", true)]
    public class MuddyPuddlesEasy : ScriptedGame
    {
        BTD6GamePlayer _player;

        public MuddyPuddlesEasy(BTD6GamePlayer player)
        {
            _player = player;
        }

        public void DoActions()
        {
            _player.Wait(1000);

            _player.StartRound();
            _player.ToggleFastForward();
            _player.PlaceTower(TowerType.Dart, 340, 240);

            _player.Wait(8300);
            _player.PlaceTower(TowerType.Hero, 1100, 200);

            _player.Wait(8000);
            var sniper1 = _player.PlaceTower(TowerType.Sniper, 1100, 300);

            _player.Wait(10000);
            _player.UpgradeTower(sniper1, UpgradePath.Bottom);

            _player.Wait(11500);
            _player.UpgradeTower(sniper1, UpgradePath.Bottom);

            _player.Wait(10500);
            _player.UpgradeTower(sniper1, UpgradePath.Top);

            _player.Wait(93000);
            _player.UpgradeTower(sniper1, UpgradePath.Bottom);

            _player.Wait(56000);
            _player.UpgradeTower(sniper1, UpgradePath.Bottom);

            _player.Wait(13000);
            _player.UpgradeTower(sniper1, UpgradePath.Top);

            _player.Wait(3000);
            var sniper2 = _player.PlaceTower(TowerType.Sniper, 1100, 400);

            _player.Wait(4000);
            _player.UpgradeTower(sniper2, UpgradePath.Middle);

            _player.Wait(4000);
            _player.UpgradeTower(sniper2, UpgradePath.Top);

            _player.Wait(13000);
            _player.UpgradeTower(sniper2, UpgradePath.Top);

            _player.Wait(3000);
            _player.UpgradeTower(sniper2, UpgradePath.Middle);

            _player.Wait(33000);
            _player.UpgradeTower(sniper2, UpgradePath.Middle);

            _player.Wait(3000);
            var sniper3 = _player.PlaceTower(TowerType.Sniper, 1100, 500);

            _player.Wait(3000);
            _player.UpgradeTower(sniper3, UpgradePath.Top);

            _player.Wait(15000);
            _player.UpgradeTower(sniper3, UpgradePath.Top);

            _player.Wait(3000);
            _player.UpgradeTower(sniper3, UpgradePath.Bottom);

            _player.Wait(3000);
            _player.UpgradeTower(sniper3, UpgradePath.Bottom);

            _player.Wait(15000);
            _player.UpgradeTower(sniper3, UpgradePath.Bottom);
            
            _player.Wait(45000);
            //_player.GoFreePlay();

            _player.Wait(3000);
            //_player.Restart();
            _player.Wait(3000);

            _player.Wait(1000);
        }

        public override string ToString()
        {
            return "Muddy Puddles [EASY] {Benjamin}";
        }
    }
}
