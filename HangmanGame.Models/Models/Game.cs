using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame.Models.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int WordId { get; set; }

        [Required]
        public Word Word { get; set; }

        public string LettersGuessed { get; set; }
    }
}
