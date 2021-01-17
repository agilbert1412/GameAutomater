using System.Collections.Generic;
using System.IO;

namespace BTD6Automater
{
    public class ScriptLoader
    {

        private static string[] SCRIPT_PATHS = new string[]
        {
            @"..\..\..\Scripts\",
            @".\Scripts\"
        };

        public IEnumerable<ScriptedGame> LoadScripts(BTD6GamePlayer btd6GamePlayer, string fileExtension)
        {
            foreach (var path in SCRIPT_PATHS)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var files = Directory.EnumerateFiles(path, "*" + fileExtension, SearchOption.TopDirectoryOnly);
                foreach (var strategyFile in files)
                {
                    yield return new ParsedScript(btd6GamePlayer, strategyFile);
                }
            }
        }
    }
}
