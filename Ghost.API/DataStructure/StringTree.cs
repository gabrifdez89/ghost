using System.Collections.Generic;

namespace Ghost.API.DataStructure
{
    public struct StringTree
    {
        private StringTreeNode _root;

        public StringTree(IEnumerable <string> words)
        {
            _root = new StringTreeNode('\0');
            Build(words);
        }

        public StringTreeNode Find(string text)
        {
            return _root.Find(text);
        }

        private void Build(IEnumerable<string> words)
        {
            foreach (string word in words)
            {
                _root.Add(word);
            }
        }
    }
}
