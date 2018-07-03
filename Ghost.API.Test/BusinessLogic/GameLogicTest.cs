using System;
using System.Collections.Generic;
using System.Text;
using Ghost.API.BusinessLogic;
using Ghost.API.Persistence;
using Moq;
using Xunit;

namespace Ghost.API.Test.BusinessLogic
{
    public class GameLogicTest
    {
        private Mock<IWordsRepository> wordsRepositoryMock;
        private GameLogic gameLogic;

        public GameLogicTest()
        {
            wordsRepositoryMock = new Mock<IWordsRepository>();
            gameLogic = new GameLogic(wordsRepositoryMock.Object);
        }

        [Fact]
        public void CheckLosePlayShouldReturnTrueIfThereAreNoWordsStartingAsSpecified()
        {
            string text = "qwerqwer";
            wordsRepositoryMock
                .Setup(repo => repo.GetWordsStartingLike(text))
                .Returns(new List<string>());

            var result = gameLogic.CheckLosePlay(text);

            Assert.True(result);
        }

        [Fact]
        public void CheckLosePlayShouldReturnTrueIfTheSpecifiedTextIsAWord()
        {
            string text = "qwerqwer";
            wordsRepositoryMock
                .Setup(repo => repo.ContainsWord(text))
                .Returns(true);

            var result = gameLogic.CheckLosePlay(text);

            Assert.True(result);
        }

        [Fact]
        public void CheckLosePlayShouldReturnFalseOtherwise()
        {
            string text = "qwerqwer";
            wordsRepositoryMock
                .Setup(repo => repo.GetWordsStartingLike(text))
                .Returns(new List<string>() { "qwerqwero", "qwerqwera" });
            wordsRepositoryMock
                .Setup(repo => repo.ContainsWord(text))
                .Returns(false);

            var result = gameLogic.CheckLosePlay(text);

            Assert.False(result);
        }
    }
}
