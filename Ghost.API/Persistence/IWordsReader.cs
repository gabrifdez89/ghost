using System.Collections.Generic;

namespace Ghost.API.Persistence
{
    public interface IWordsReader
    {
        IEnumerable<string> GetAllWords();
    }
}
