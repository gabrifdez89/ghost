using System.Collections.Generic;
using Ghost.API.BusinessLogic;
using Ghost.API.Persistence;
using Moq;
using Xunit;

namespace Ghost.API.Test.BusinessLogic
{
    public class OptimalGhostPlayerTest
    {
        private Mock<IWordsRepository> wordsRepositoryMock;
        private OptimalGhostPlayer player;

        public OptimalGhostPlayerTest()
        {
            wordsRepositoryMock = new Mock<IWordsRepository>();
        }

        [Fact]
        public void PlayShouldReturnTheWinningOptionWhenThereIsOnlyOne()
        {
            wordsRepositoryMock
                .Setup(repo => repo.GetAllWords())
                .Returns(new List<string>() { "aba", "aca", "acb" });
            player = new OptimalGhostPlayer(wordsRepositoryMock.Object);

            var result = player.Play("a");

            Assert.Equal("ab", result);
        }

        [Fact]
        public void PlayShouldReturnOneOfTheWinningOptionsWhenThereAreSome()
        {
            wordsRepositoryMock
                .Setup(repo => repo.GetAllWords())
                .Returns(new List<string>() { "aba", "aca", "acb", "ada" });
            player = new OptimalGhostPlayer(wordsRepositoryMock.Object);

            var result = player.Play("a");

            Assert.True(result.Equals("ab") || result.Equals("ad"));
        }

        [Fact]
        public void PlayShouldReturnTheLongestLastingOptionWhenThereIsNoWinningOption()
        {
            wordsRepositoryMock
                .Setup(repo => repo.GetAllWords())
                .Returns(new List<string>() { "aba", "abb", "aca", "acb", "acaa" });
            player = new OptimalGhostPlayer(wordsRepositoryMock.Object);

            var result = player.Play("a");

            Assert.Equal("ac", result);
        }

        [Fact]
        public void PlayShouldNotTakeAsAWinningOptionIfTurnsDoNotGrantWinning()
        {
            wordsRepositoryMock
                .Setup(repo => repo.GetAllWords())
                .Returns(new List<string>() { "abab", "aca" });
            player = new OptimalGhostPlayer(wordsRepositoryMock.Object);

            var result = player.Play("a");

            Assert.Equal("ac", result);
        }

        [Fact]
        public void PlayShouldNotTakeAsAWinningOptionIfASubsetOfThatBranchIsAlsoAWord()
        {
            wordsRepositoryMock
                .Setup(repo => repo.GetAllWords())
                .Returns(new List<string>() { "abababa", "abab", "acaca" });
            player = new OptimalGhostPlayer(wordsRepositoryMock.Object);

            var result = player.Play("a");

            Assert.Equal("ac", result);
        }
    }
}
