using HangmanGame.DataAccess.Data;
using HangmanGame.DataAccess.Repository;
using HangmanGame.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame.Tests
{
    /// <summary>
    /// NOTE: Words are being seeded in the migrations for the purpose of this demo. There is 13 words in total.
    /// </summary>
    public class BaseTest
    {
        private DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "HangmanGameTest")
            .Options;

        public ApplicationDbContext Context;
        public GameRepository GameRepository;
        public WordRepository WordRepository;

        public void Setup()
        {
            Context = new ApplicationDbContext(dbContextOptions);
            Context.Database.EnsureCreated();
            SeedDatabase();

            GameRepository = new GameRepository(Context);
            WordRepository = new WordRepository(Context);
        }

        public void TearDown()
        {
            Context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var games = new List<Game>()
            {
                new Game()
                {
                    WordId = 1,
                    LettersGuessed = ""
                },
                new Game()
                {
                    WordId = 2,
                    LettersGuessed = "asd"
                },
                new Game()
                {
                    WordId = 3,
                    LettersGuessed = "aeio"
                }
            };

            Context.Games.AddRange(games);
            Context.SaveChanges();
        }
    }
}
