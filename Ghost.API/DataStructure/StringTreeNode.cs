using System;
using System.Collections.Generic;
using System.Linq;

namespace Ghost.API.DataStructure
{
    public struct StringTreeNode
    {
        private char _letter;
        private Dictionary<char, StringTreeNode> _children;
        private bool _isWord { get; set; }

        public StringTreeNode(char letter = '\0')
        {
            _letter = letter;
            _children = new Dictionary<char, StringTreeNode>();
            _isWord = false;
        }

        public void Add(string text)
        {
            char firstLetter = text[0];

            if (!_children.ContainsKey(firstLetter))
            {
                StringTreeNode newNode = new StringTreeNode(firstLetter);
                _children.Add(firstLetter, newNode);
            }

            if (text.Length > 1)
            {
                _children[firstLetter].Add(text.Substring(1));
            }
            else
            {
                var child = _children[firstLetter];

                child.IsWord = true;

                _children[firstLetter] = child;
            }
        }

        public StringTreeNode Find(string text)
        {
            StringTreeNode node;

            if (_children.ContainsKey(text[0]))
            {
                node = _children[text[0]];
            }
            else
            {
                throw new KeyNotFoundException("Word not found");
            }

            if (text.Length > 1)
            {
                return node.Find(text.Substring(1));
            }

            return node;
        }

        public IEnumerable<StringTreeNode> GetChildren()
        {
            return _children.Values;
        }

        public bool LeadsToASingleWord()
        {
            if (_children.Count().Equals(0))
            {
                return true;
            }

            if (_children.Count().Equals(1))
            {
                return !IsWord && _children.Values.First().LeadsToASingleWord();
            }

            return false;
        }

        public int GetDeepOfChildren()
        {
            int maxDeep = 0;

            foreach(var childNode in _children.Values)
            {
                var deep = childNode.GetDeepOfChildren() + 1;
                if (deep > maxDeep)
                {
                    maxDeep = deep;
                }
            }

            return maxDeep;
        }

        public char GetChar()
        {
            return _letter;
        }

        public bool IsWord
        {
            get
            {
                return _isWord;
            }
            set
            {
                _isWord = value;
            }
        }
    }
}
