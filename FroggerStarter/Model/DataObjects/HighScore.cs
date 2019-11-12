using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FroggerStarter.Model
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
            this.FullDescription = $"Score: {this.GameScore}   |   Name: {this.Name}   |   Level: {this.GameLevel}";
        }

        public void SortDescriptionDefault()
        {
            this.FullDescription = $"Score: {this.GameScore}   |   Name: {this.Name}   |   Level: {this.GameLevel}";
        }

        public void SortDescriptionByLevel()
        {
            this.FullDescription = $"Level: {this.GameLevel}   |   Score: {this.GameScore}   |   Name: {this.Name}";
        }

        public void SortDescriptionByName()
        {
            this.FullDescription = $"Name: {this.Name}   |   Score: {this.GameScore}   |   Level: {this.GameLevel}";
        }
    }
}
