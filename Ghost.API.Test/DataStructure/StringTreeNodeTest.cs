using System.Collections.Generic;
using Ghost.API.DataStructure;
using Xunit;

namespace Ghost.API.Test.DataStructure
{
    public class StringTreeNodeTest
    {
        private StringTreeNode node;

        public StringTreeNodeTest()
        {
            node = new StringTreeNode('\0');
        }

        [Fact]
        public void AddShouldCreateAChildNodeForTheFirstLetterIfItDoesNotExist()
        {
            node.Add("a");

            Assert.IsType<StringTreeNode>(node.Find("a"));
        }

        [Fact]
        public void AddShouldNodesForConsecutiveLetters()
        {
            node.Add("ab");

            Assert.IsType<StringTreeNode>(node.Find("ab"));
        }

        [Fact]
        public void AddShouldSetAsWordTheNodeForTheLastLetterOfAWord()
        {
            node.Add("home");

            var eNode = node.Find("home");
            Assert.True(eNode.IsWord);
        }

        [Fact]
        public void FindShouldThrowAKeyNotFoundExceptionIfWordIsNotFound()
        {
            node.Add("home");

            var ex = Assert.Throws<KeyNotFoundException>(() => node.Find("sleep"));
            Assert.Equal("Word not found", ex.Message);
        }

        [Fact]
        public void LeadsToASingleWordShouldReturnTrueWhenItHasNoChildren()
        {
            Assert.True(node.LeadsToASingleWord());
        }

        [Fact]
        public void LeadsToASingleWordShouldReturnTrueWhenThereIsOnlyABranchAndAWord()
        {
            node.Add("home");

            Assert.True(node.LeadsToASingleWord());
        }

        [Fact]
        public void LeadsToASingleWordShouldReturnFalseWhenThereIsOnlyABranchButSomeWords()
        {
            node.Add("taboos");
            node.Add("taboo");

            Assert.False(node.LeadsToASingleWord());
        }

        [Fact]
        public void LeadsToASingleWordShouldReturnFalseWhenThereAreVariousBranches()
        {
            node.Add("taboos");
            node.Add("tomato");

            Assert.False(node.LeadsToASingleWord());
        }

        [Fact]
        public void GetDeepOfChildrenShouldReturnTheDeepestLevelOfChildren()
        {
            node.Add("home");
            node.Add("sleeping");

            Assert.Equal(8, node.GetDeepOfChildren());
        }
    }
}
