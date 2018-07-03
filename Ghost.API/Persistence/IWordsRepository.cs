using System;
using System.Collections.Generic;

namespace Ghost.API.Persistence
{
    public interface IWordsRepository
    {
        IEnumerable<String> GetWordsStartingLike(string start);
        IEnumerable<String> GetAllWords();
        bool ContainsWord(string word);
    }
}
