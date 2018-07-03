using System.Linq;
using Ghost.API.Persistence;

namespace Ghost.API.BusinessLogic
{
    public class GameLogic : IGameLogic
    {
        private IWordsRepository _wordsRepository;

        public GameLogic(IWordsRepository wordsRepository)
        {
            _wordsRepository = wordsRepository;
        }

        public bool CheckLosePlay(string text)
        {
            if (_wordsRepository.GetWordsStartingLike(text).Count().Equals(0))
            {
                return true;
            }

            if (_wordsRepository.ContainsWord(text))
            {
                return true;
            }

            return false;
        }
    }
}
