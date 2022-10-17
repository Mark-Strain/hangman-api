using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame.Models.ViewModels
{
    public class GuessViewModel
    {
        public int GameId { get; set; }
        public bool Correct { get; set; }
        public string Word { get; set; }
    }
}
