using Ghost.API.BusinessLogic;
using Ghost.API.Models;
using Ghost.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Ghost.API.Controllers
{
    [Route("api/play")]
    public class PlayController : Controller
    {
        private IWordsRepository _wordsRepository;
        private IGameLogic _gameLogic;
        private IGhostPlayer _ghostPlayer;

        public PlayController(IWordsRepository wordsRepository, IGameLogic gameLogic, IGhostPlayer ghostPlayer)
        {
            _wordsRepository = wordsRepository;
            _gameLogic = gameLogic;
            _ghostPlayer = ghostPlayer;
        }

        [HttpPost()]
        public IActionResult PostPlay([FromBody] PlayDto play)
        {
            if (!ModelState.IsValid || play == null)
            {
                return BadRequest(ModelState);
            }

            if (_gameLogic.CheckLosePlay(play.Text))
            {
                return Ok(new PlayResultDto()
                {
                    Text = play.Text,
                    Win = false,
                    Lose = true
                });
            }

            string text = _ghostPlayer.Play(play.Text);

            return Ok(new PlayResultDto() {
                Text = text,
                Win = _gameLogic.CheckLosePlay(text),
                Lose = false
            });
        }
    }
}
