using System.Collections.Generic;

namespace FroggerStarter.Model.DataObjects
{
    public class HighScoreBoard
    {
        private readonly IList<HighScore> highScoreBoard;

        public IList<HighScore> Scores => this.highScoreBoard;

        public int Count => this.highScoreBoard.Count;

        public HighScoreBoard()
        {
            this.highScoreBoard = new List<HighScore>();
            this.createScoreBoard();
        }

        private void createScoreBoard()
        {
            var score1 = new HighScore("John", 1500, 2);
            var score2 = new HighScore("Max", 760, 1);
            var score3 = new HighScore("Amanda", 2000, 3);
            var score4 = new HighScore("Sally", 2500, 3);

            this.Add(score1);
            this.Add(score2);
            this.Add(score3);
            this.Add(score4);
        }

        public void Add(HighScore score)
        {
            this.highScoreBoard.Add(score);
        }

        public void RemoveAll()
        {
            this.highScoreBoard.Clear();
        }
    }
}
