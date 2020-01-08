using BTD6Automater;

namespace GameAutomater
{
    public class ScriptExecuter
    {
        public void ExecuteScript(ScriptedGame strategy, int loops)
        {
            var loopNum = 0;

            while (loopNum < loops || loops == 0)
            {
                ExecuteScript(strategy);
            }
        }

        public void ExecuteScript(ScriptedGame strategy)
        {
            strategy.DoActions();
        }
    }
}
