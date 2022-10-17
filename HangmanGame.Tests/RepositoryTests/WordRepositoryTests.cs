using HangmanGame.DataAccess.Data;
using HangmanGame.DataAccess.Repository;
using HangmanGame.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame.Tests.RepositoryTests
{
    public class WordRepositoryTests : BaseTest
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
        public void WordRepository_GetById_Valid()
        {
            var word = WordRepository.GetById(2);

            Assert.IsTrue(word.Id == 2);
        }

        [Test]
        public void WordRepository_GetAll_Valid()
        {
            var word = WordRepository.GetAll().ToList();

            Assert.IsTrue(word.Count == 13);
        }
    }
}
