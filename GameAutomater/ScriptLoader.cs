using BTD6Automater;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
