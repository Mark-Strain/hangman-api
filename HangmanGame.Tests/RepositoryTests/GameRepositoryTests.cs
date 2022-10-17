using HangmanGame.DataAccess.Data;
using HangmanGame.DataAccess.Repository;
using HangmanGame.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame.Tests.RepositoryTests
{
    public class GameRepositoryTests : BaseTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            base.Setup();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            base.TearDown();
        }

        [Test]
        public void GameRepository_Add_Valid()
        {
            var word = Context.Words.FirstOrDefault();

            var game = new Game()
            {
                Word = word,
                LettersGuessed = ""
            };

            GameRepository.Add(game);
            GameRepository.SaveChanges();

            var games = Context.Games.ToList();

            Assert.IsTrue(games.Count == 4);
        }

        [Test]
        public void GameRepository_GetById_Valid()
        {
            var game = GameRepository.GetById(2);

            Assert.IsTrue(game.Id == 2);
        }
    }
}
