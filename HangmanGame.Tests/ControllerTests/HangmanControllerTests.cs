using HangmanGame.Controllers;
using HangmanGame.Models.Models;
using HangmanGame.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NuGet.Frameworks;
using NUnit.Framework;

namespace HangmanGame.Tests.ControllerTests
{
    public class HangmanControllerTests : BaseTest
    {
        private HangmanController _hangmanController;

        [OneTimeSetUp]
        public void Setup()
        {
            base.Setup();
            _hangmanController = new HangmanController(GameRepository, WordRepository);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Context.Database.EnsureDeleted();
        }

        [Test]
        public void IsGuessInWord_True()
        {
            var word = "racecar";
            var guess = 'c';

            var result = _hangmanController.IsGuessInWord(word, guess);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsGuessInWord_False()
        {
            var word = "racecar";
            var guess = 'x';

            var result = _hangmanController.IsGuessInWord(word, guess);

            Assert.IsFalse(result);
        }

        [Test]
        public void GenerateHangmanWordFormat_NoGuesses()
        {
            var word = "racecar";
            var lettersGuessed = "";

            var result = _hangmanController.GenerateHangmanWordFormat(word, lettersGuessed);

            Assert.AreEqual("_ _ _ _ _ _ _", result);
        }

        [Test]
        public void GenerateHangmanWordFormat_CorrectGuesses()
        {
            var word = "racecar";
            var lettersGuessed = "re";

            var result = _hangmanController.GenerateHangmanWordFormat(word, lettersGuessed);

            Assert.AreEqual("r _ _ e _ _ r", result);
        }

        [Test]
        public void GenerateHangmanWordFormat_NoCorrectGuesses()
        {
            var word = "racecar";
            var lettersGuessed = "xfs";

            var result = _hangmanController.GenerateHangmanWordFormat(word, lettersGuessed);

            Assert.AreEqual("_ _ _ _ _ _ _", result);
        }

        [Test]
        public void RandomWord_Valid()
        {
            var result = _hangmanController.GetRandomWord();

            Assert.IsNotNull(result);
        }

        [Test]
        public void HTTPGET_GetSolution_ReturnOk()
        {
            var result = _hangmanController.GetSolution(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void HTTPGET_GetSolution_ReturnSolution()
        {
            var result = _hangmanController.GetSolution(1);

            if(result != null)
            {
                var resultData = (result as OkObjectResult).Value as SolutionViewModel;

                Assert.AreEqual("fade", resultData.Solution);
            }
            else
            {
                Assert.Fail("Result is Null");
            }
        }

        [Test]
        public void HTTPGET_GetSolution_ReturnNotFound()
        {
            var result = _hangmanController.GetSolution(5);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void HTTPGET_GetGame_ReturnGame()
        {
            var result = _hangmanController.GetGame(2);

            if (result != null)
            {
                var resultData = (result as OkObjectResult).Value as GameViewModel;

                Assert.AreEqual(2, resultData.GameId);
            }
            else
            {
                Assert.Fail("Result is Null");
            }
        }

        [Test]
        public void HTTPGET_GetGame_ReturnOk()
        {
            var result = _hangmanController.GetGame(2);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void HTTPGET_GetGame_ReturnNotFound()
        {
            var result = _hangmanController.GetGame(5);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void HTTPPOST_CreateGuess_ReturnOk()
        {
            var result = _hangmanController.CreateGuess(1, new Guess { Letter = 'a' });

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void HTTPPOST_CreateGuess_ReturnNotFound()
        {
            var result = _hangmanController.CreateGuess(20, new Guess { Letter = 'a' });

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void HTTPPOST_CreateGuess_SpecialCharacter_ReturnOk()
        {
            var result = _hangmanController.CreateGuess(1, new Guess { Letter = '#' });

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void HTTPPOST_CreateGuess_CorrectGuess_ReturnWord()
        {
            var result = _hangmanController.CreateGuess(1, new Guess { Letter = 'f' });

            if (result != null)
            {
                var resultData = (result as OkObjectResult).Value as GuessViewModel;

                Assert.AreEqual("f _ _ _", resultData.Word);
            }
            else
            {
                Assert.Fail("Result is Null");
            }
        }

        [Test]
        public void HTTPPOST_CreateGuess_GuessError()
        {
            _hangmanController.CreateGuess(2, new Guess { Letter = 'f' });

            var result = _hangmanController.CreateGuess(2, new Guess { Letter = 'f' });

            if (result != null)
            {
                var resultData = (result as OkObjectResult).Value;

                Assert.That(resultData, Is.TypeOf<GuessErrorViewModel>());
            }
            else
            {
                Assert.Fail("Result is Null");
            }
        }

        [Test]
        public void HTTPPOST_CreateGame_ReturnOk()
        {
            var result = _hangmanController.CreateGame();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void HTTPPOST_CreateGame_GameExists()
        {
            var result = _hangmanController.CreateGame();

            var resultData = (result as OkObjectResult).Value as GameViewModel;

            var games = GameRepository.GetById(resultData.GameId);

            Assert.IsNotNull(games);
        }
    }
}