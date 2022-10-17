using HangmanGame.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame.DataAccess.Repository.IRepository
{
    public interface IWordRepository
    {
        Word GetById(int id);
        IEnumerable<Word> GetAll();
    }
}
