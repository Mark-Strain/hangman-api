using HangmanGame.DataAccess.Data;
using HangmanGame.DataAccess.Repository;
using HangmanGame.DataAccess.Repository.IRepository;
using HangmanGame.Models.Models;
using HangmanGame.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HangmanGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangmanController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly IWordRepository _wordRepository;

        public HangmanController(IGameRepository gameRepository, IWordRepository wordRepository)
        {
            _gameRepository = gameRepository;
            _wordRepository = wordRepository;
        }

        [HttpPost]
        public IActionResult CreateGame()
        {
            try
            {
                var word = GetRandomWord();

                if (word == null)
                {
                    return StatusCode(500);
                }

                var game = new Game()
                {
                    Word = word,
                    LettersGuessed = ""
                };

                _gameRepository.Add(game);
                _gameRepository.SaveChanges();

                var hangmanWord = GenerateHangmanWordFormat(game.Word.Value, "");

                return Ok(new GameViewModel
                {
                    GameId = game.Id,
                    Word = hangmanWord
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("{id}/guess")]
        public IActionResult CreateGuess(int id, [FromBody]Guess guess)
        {
            try
            {
                var guessLetterToLower = char.ToLower(guess.Letter);

                var game = _gameRepository.GetById(id);

                if (game == null)
                {
                    return NotFound();
                }

                var word = _wordRepository.GetById(game.WordId);

                if (word == null)
                {
                    return StatusCode(500);
                }

                if (game.LettersGuessed.Contains(guessLetterToLower))
                {
                    return Ok(new GuessErrorViewModel
                    {
                        Message = "Letter already guessed"
                    });
                }

                game.LettersGuessed += guessLetterToLower;
                _gameRepository.SaveChanges();

                var hangmanWord = GenerateHangmanWordFormat(word.Value, game.LettersGuessed);
                var correct = IsGuessInWord(word.Value, guessLetterToLower);

                return Ok(new GuessViewModel
                {
                    GameId = game.Id,
                    Correct = correct,
                    Word = hangmanWord
                });
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetGame(int id)
        {
            try
            {
                var game = _gameRepository.GetById(id);

                if(game == null)
                {
                    return NotFound();
                }

                var word = _wordRepository.GetById(game.WordId);

                if (word == null)
                {
                    return StatusCode(500);
                }

                var hangmanWord = GenerateHangmanWordFormat(word.Value, game.LettersGuessed);

                return Ok(new GameViewModel
                {
                    GameId = game.Id,
                    Word = hangmanWord
                });
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}/solution")]
        public IActionResult GetSolution(int id)
        {
            try
            {
                var game = _gameRepository.GetById(id);

                if (game == null)
                {
                    return NotFound();
                }

                var word = _wordRepository.GetById(game.WordId);

                if (word == null)
                {
                    return StatusCode(500);
                }

                return Ok(new SolutionViewModel
                {
                    GameId = game.Id,
                    Solution = game.Word.Value
                });
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public Word GetRandomWord()
        {
            var words = _wordRepository.GetAll().ToArray();

            if (words.Count() > 0)
            {
                var random = new Random();
                var randomWordIndex = random.Next(words.Count());

                return words[randomWordIndex];
            }
            else
            {
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GenerateHangmanWordFormat(string word, string lettersGuessed)
        {
            string returnvalue = "";

            foreach(var letter in word.ToLower())
            {
                if (lettersGuessed.Contains(letter))
                {
                    returnvalue += $"{letter} ";
                }
                else
                {
                    returnvalue += "_ ";
                }
            }

            return returnvalue.TrimEnd();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public bool IsGuessInWord(string word, char guess)
        {
            return word.ToLower().Contains(guess);
        }
    }
}
