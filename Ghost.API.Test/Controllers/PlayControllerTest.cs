using Ghost.API.BusinessLogic;
using Ghost.API.Controllers;
using Ghost.API.Models;
using Ghost.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Ghost.API.Test.Controllers
{
    public class PlayControllerTest
    {
        private Mock<IWordsRepository> wordsRepositoryMock;
        private Mock<IGameLogic> gameLogicMock;
        private Mock<IGhostPlayer> ghostPlayerMock;
        private PlayController controller;

        public PlayControllerTest()
        {
            wordsRepositoryMock = new Mock<IWordsRepository>();
            gameLogicMock = new Mock<IGameLogic>();
            ghostPlayerMock = new Mock<IGhostPlayer>();
            controller = new PlayController(wordsRepositoryMock.Object, gameLogicMock.Object, ghostPlayerMock.Object);
        }

        [Fact]
        public void PostPlayShouldReturnBadRequestIfPlayDtoIsNull()
        {
            var result = controller.PostPlay(null);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PostPlayShouldReturnBadRequestIfModelStateIsNotValid()
        {
            var playDtoMock = new PlayDto() {
                Text = "123"
            };
            controller.ModelState.AddModelError("test", "test");

            var result = controller.PostPlay(playDtoMock);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PostPlayShouldReturnOkIfModelStateIsValid()
        {
            var playDtoMock = new PlayDto()
            {
                Text = "a"
            };

            var result = controller.PostPlay(playDtoMock);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void PostPlayShouldReturnTheRightOkResponseIfPlayerHasLost()
        {
            var playDtoMock = new PlayDto()
            {
                Text = "qwerqwer"
            };
            gameLogicMock
                .Setup(gameLogic => gameLogic.CheckLosePlay(playDtoMock.Text))
                .Returns(true);

            IActionResult result = controller.PostPlay(playDtoMock);

            var playResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<PlayResultDto>(playResult.Value);
            Assert.True(model.Lose);
            Assert.False(model.Win);
            Assert.Equal(playDtoMock.Text, model.Text);
        }

        [Fact]
        public void PostPlayShouldReturnAnOkResponseWithThePlayFromSystemPlayerIfPlayerHasNotLost()
        {
            var playDtoMock = new PlayDto()
            {
                Text = "a"
            };
            gameLogicMock
                .Setup(gameLogic => gameLogic.CheckLosePlay(playDtoMock.Text))
                .Returns(false);
            ghostPlayerMock
                .Setup(ghostPlayer => ghostPlayer.Play(playDtoMock.Text))
                .Returns("ab");

            IActionResult result = controller.PostPlay(playDtoMock);

            var playResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<PlayResultDto>(playResult.Value);
            Assert.False(model.Lose);
            Assert.False(model.Win);
            Assert.Equal("ab", model.Text);
        }

        [Fact]
        public void PostPlayShouldCheckIfTheSystemPlayerHasLost()
        {
            var playDtoMock = new PlayDto()
            {
                Text = "a"
            };
            gameLogicMock
                .Setup(gameLogic => gameLogic.CheckLosePlay(playDtoMock.Text))
                .Returns(false);
            ghostPlayerMock
                .Setup(ghostPlayer => ghostPlayer.Play(playDtoMock.Text))
                .Returns("ab");
            gameLogicMock
                .Setup(gameLogic => gameLogic.CheckLosePlay("ab"))
                .Returns(true);

            IActionResult result = controller.PostPlay(playDtoMock);

            var playResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<PlayResultDto>(playResult.Value);
            Assert.False(model.Lose);
            Assert.True(model.Win);
            Assert.Equal("ab", model.Text);
        }
    }
}
