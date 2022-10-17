using HangmanGame.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>().HasData(
                new Word { Id = 1, Value = "fade" },
                new Word { Id = 2, Value = "decorative" },
                new Word { Id = 3, Value = "credibility" },
                new Word { Id = 4, Value = "killer" },
                new Word { Id = 5, Value = "foreigner" },
                new Word { Id = 6, Value = "notice" },
                new Word { Id = 7, Value = "hiccup" },
                new Word { Id = 8, Value = "eternal" },
                new Word { Id = 9, Value = "age" },
                new Word { Id = 10, Value = "key" },
                new Word { Id = 11, Value = "surface" },
                new Word { Id = 12, Value = "activate" },
                new Word { Id = 13, Value = "discount" }
                );
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}
