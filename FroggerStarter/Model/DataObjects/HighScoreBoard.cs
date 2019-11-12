using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FroggerStarter.Model
{
    
    public class HighScoreBoard
    {
        private readonly IList<HighScore> highScoreBoard;

        public IList<HighScore> Scores => this.highScoreBoard;

        public int Count => this.highScoreBoard.Count;

        public HighScoreBoard()
        {
            this.highScoreBoard = new List<HighScore>();
        }

        public void CreateDefaultScores()
        {
            var score1 = new HighScore("Shawn", 3100, 1);
            var score2 = new HighScore("Amanda", 21000, 2);
            var score3 = new HighScore("Kelly", 33150, 3);

            this.Add(score1);
            this.Add(score2);
            this.Add(score3);
        }

        public void Add(HighScore score)
        {
            this.highScoreBoard.Add(score);
        }
    }
}
