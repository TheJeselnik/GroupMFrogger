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

        public void Add(HighScore score)
        {
            this.highScoreBoard.Add(score);
        }
    }
}
