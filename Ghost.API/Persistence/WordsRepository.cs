using System.Collections.Generic;
using System.Linq;

namespace Ghost.API.Persistence
{
    public class WordsRepository : IWordsRepository
    {
        private IWordsReader _wordsReader;
        private IEnumerable<string> _allWords;

        public WordsRepository(IWordsReader wordsReader)
        {
            _wordsReader = wordsReader;
            _allWords = wordsReader.GetAllWords();
        }

        public IEnumerable<string> GetWordsStartingLike(string start)
        {
            return _allWords.ToList().Where(w => w.StartsWith(start));
        }

        public IEnumerable<string> GetAllWords()
        {
            return _allWords;
        }

        public bool ContainsWord(string word)
        {
            return _allWords.ToList<string>().Contains(word);
        }
    }
}
