using System;
using System.Collections.Generic;
using System.Linq;
using Ghost.API.DataStructure;
using Ghost.API.Persistence;

namespace Ghost.API.BusinessLogic
{
    public class OptimalGhostPlayer : IGhostPlayer
    {
        private IWordsRepository _wordsRepository;
        private StringTree _wordsDictionary;
        static Random _random;

        public OptimalGhostPlayer(IWordsRepository wordsRepository)
        {
            _wordsRepository = wordsRepository;
            _wordsDictionary = new StringTree(wordsRepository.GetAllWords());
            _random = new Random();
        }

        public string Play(string text)
        {
            StringTreeNode node = _wordsDictionary.Find(text);

            List<StringTreeNode> winningOptions = GetWinningOptions(node);

            if (winningOptions.Any())
            {
                return GetRandomOption(text, winningOptions);
            }

            return GameExtendingLongestOption(text, node);
        }

        private string GameExtendingLongestOption(string text, StringTreeNode node)
        {
            var deep = 0;
            int maxDeep = 1;
            StringTreeNode option = node.GetChildren().ElementAt(_random.Next(node.GetChildren().Count()));

            foreach (var childNode in node.GetChildren())
            {
                deep = childNode.GetDeepOfChildren() + 1;
                if (deep > maxDeep && !childNode.IsWord)
                {
                    maxDeep = deep;
                    option = childNode;
                }
            }

            return text + option.GetChar();
        }

        private string GetRandomOption(string text, List<StringTreeNode> winningOptions)
        {
            return text + winningOptions.ElementAt(_random.Next(winningOptions.Count())).GetChar();
        }

        private List<StringTreeNode> GetWinningOptions(StringTreeNode node)
        {
            List<StringTreeNode> winningOptions = new List<StringTreeNode>();

            foreach (var childNode in node.GetChildren())
            {
                if (childNode.LeadsToASingleWord() && childNode.GetDeepOfChildren() % 2 != 0 && !childNode.IsWord)
                {
                    winningOptions.Add(childNode);
                }
            }

            return winningOptions;
        }
    }
}
