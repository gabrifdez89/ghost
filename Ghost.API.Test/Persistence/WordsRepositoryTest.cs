using System.Collections.Generic;
using Ghost.API.Persistence;
using Moq;
using Xunit;

namespace Ghost.API.Test.Persistence
{
    public class WordsRepositoryTest
    {
        private Mock<IWordsReader> wordsReaderMock;
        private WordsRepository repo;

        public WordsRepositoryTest()
        {
            wordsReaderMock = new Mock<IWordsReader>();
        }

        [Fact]
        public void GetAllWordsShouldReturnAllTheWords()
        {
            var words = new List<string>() { "house", "car" };
            wordsReaderMock
                .Setup(reader => reader.GetAllWords())
                .Returns(words);
            repo = new WordsRepository(wordsReaderMock.Object);

            Assert.Equal(words, repo.GetAllWords());
        }

        [Fact]
        public void ContainsWordShouldReturnTrueIfContained()
        {
            var words = new List<string>() { "house", "car" };
            wordsReaderMock
                .Setup(reader => reader.GetAllWords())
                .Returns(words);
            repo = new WordsRepository(wordsReaderMock.Object);

            Assert.True(repo.ContainsWord("house"));
        }

        [Fact]
        public void ContainsWordShouldReturnFalseIfNotContained()
        {
            var words = new List<string>() { "house", "car" };
            wordsReaderMock
                .Setup(reader => reader.GetAllWords())
                .Returns(words);
            repo = new WordsRepository(wordsReaderMock.Object);

            Assert.False(repo.ContainsWord("motorcycle"));
        }

        [Fact]
        public void GetWordsStartingLikeShouldReturnOnlyTheRightWords()
        {
            var words = new List<string>() { "ho", "house", "car" };
            wordsReaderMock
                .Setup(reader => reader.GetAllWords())
                .Returns(words);
            repo = new WordsRepository(wordsReaderMock.Object);

            Assert.Equal(new List<string>() { "ho", "house" }, repo.GetWordsStartingLike("ho"));
        }
    }
}
