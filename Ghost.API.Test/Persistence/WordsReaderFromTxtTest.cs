using System.Collections.Generic;
using Ghost.API.Persistence;
using Moq;
using Xunit;

namespace Ghost.API.Test.Persistence
{
    public class WordsReaderFromTxtTest
    {
        private Mock<IFileSystem> fileSystemMock;
        private WordsReaderFromTxt reader;

        public WordsReaderFromTxtTest()
        {
            fileSystemMock = new Mock<IFileSystem>();
            reader = new WordsReaderFromTxt(fileSystemMock.Object);
        }

        [Fact]
        public void ReadAllTextShouldReturnTheWordsOfTheTextSeparatedWithNewLines()
        {
            var list = new List<string>() { "fakeText", "anotherFakeText" };
            fileSystemMock
                .Setup(fs => fs.ReadAllText(It.IsAny<string>()))
                .Returns("fakeText\r\nanotherFakeText");

            var result = reader.GetAllWords();

            Assert.Equal(list, result);
        }

        [Fact]
        public void ReadAllTextShouldNotIncludeWordsOfLessThan4Letters()
        {
            var list = new List<string>() { "fakeText", "anotherFakeText" };
            fileSystemMock
                .Setup(fs => fs.ReadAllText(It.IsAny<string>()))
                .Returns("fakeText\r\nanotherFakeText\r\nfa");

            var result = reader.GetAllWords();

            Assert.Equal(list, result);
        }
    }
}
