using HangmanGame.Models.Models;

namespace HangmanGame.DataAccess.Repository.IRepository
{
    public interface IGameRepository
    {
        void Add(Game game);
        Game GetById(int id);
        void SaveChanges();
    }
}