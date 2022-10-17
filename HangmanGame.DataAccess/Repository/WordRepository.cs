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
    public class WordRepository : IWordRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public WordRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Word GetById(int id)
        {
            return _applicationDbContext.Words.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Word> GetAll()
        {
            return _applicationDbContext.Words;
        }

        public void SaveChanges()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
