using HangmanGame.DataAccess.Data;
using HangmanGame.DataAccess.Repository.IRepository;
using HangmanGame.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame.DataAccess.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GameRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Add(Game game)
        {
            _applicationDbContext.Games.Add(game);
        }

        public Game GetById(int id)
        {
            return _applicationDbContext.Games.Where(x => x.Id == id).FirstOrDefault();
        }

        public void SaveChanges()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
