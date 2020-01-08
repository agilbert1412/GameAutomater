using BTD6Automater;
using System.Collections.Generic;
using System.IO;

namespace GameAutomater
{
    public class ScriptLoader
    {
        public IEnumerable<ScriptedGame> LoadScripts(GamePlayer gamePlayer, string path, string fileExtension)
        {
            var files = Directory.EnumerateFiles(path, "*" + fileExtension, SearchOption.AllDirectories);
            foreach (var strategyFile in files)
            {
                yield return new ParsedScript(gamePlayer, strategyFile);
            }
        }
    }
}
