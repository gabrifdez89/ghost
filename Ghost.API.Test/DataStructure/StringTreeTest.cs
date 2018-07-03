using System;
using System.Collections.Generic;
using System.Text;
using Ghost.API.DataStructure;
using Moq;
using Xunit;

namespace Ghost.API.Test.DataStructure
{
    public class StringTreeTest
    {
        private IEnumerable<string> words;
        private StringTree tree;

        public StringTreeTest()
        {
            words = new List<string>() { "hellow", "car", "sleep", "home" };
            tree = new StringTree(words);
        }

        [Fact]
        public void ItShouldBuildTheTreeOnCreation()
        {
            var ex = Record.Exception(() => tree.Find("sleep"));
            Assert.Null(ex);
        }

        [Fact]
        public void FindShouldFindAStringTreeNode()
        {
            Assert.IsType<StringTreeNode>(tree.Find("sleep"));
        }
    }
}
