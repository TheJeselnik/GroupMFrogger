namespace FroggerStarter.Model.DataObjects
{
    public class HighScore
    {

        public string Name { get; private set; }

        public int GameScore { get; private set; }

        public int GameLevel { get; private set; }

        public string FullDescription { get; set; }

        public HighScore(string name, int score, int level)
        {
            this.Name = name;
            this.GameScore = score;
            this.GameLevel = level;
            this.SortDescriptionDefault();
        }

        public void SortDescriptionByScore()
        {
            this.FullDescription = $"Score: {this.GameScore}   |   Name: {this.Name}   |   Level: {this.GameLevel}";
        }

        public void SortDescriptionByLevel()
        {
            this.FullDescription = $"Level: {this.GameLevel}   |   Score: {this.GameScore}   |   Name: {this.Name}";
        }

        public void SortDescriptionDefault()
        {
            this.FullDescription = $"Name: {this.Name}   |   Score: {this.GameScore}   |   Level: {this.GameLevel}";
        }
    }
}
