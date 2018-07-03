using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ghost.API.Persistence
{
    public class WordsReaderFromTxt : IWordsReader
    {
        private IFileSystem _fileSystem;

        public WordsReaderFromTxt(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public IEnumerable<string> GetAllWords()
        {
            var data = _fileSystem.ReadAllText(Path.Combine(Environment.CurrentDirectory, "AppData/wordlist.txt"));

            IEnumerable<string> words = data.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            );

            return words.Where(w => w.Length >= 4);
        }
    }
}
