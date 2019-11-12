namespace FroggerStarter.Model.DataObjects
{
    /// <summary>
    ///     Defines the HighScore for displaying scores
    /// </summary>
    public class HighScore
    {
        #region Properties

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        ///     Gets the game score.
        /// </summary>
        /// <value>
        ///     The game score.
        /// </value>
        public int GameScore { get; }

        /// <summary>
        ///     Gets the game level.
        /// </summary>
        /// <value>
        ///     The game level.
        /// </value>
        public int GameLevel { get; }

        /// <summary>
        ///     Gets or sets the full description.
        /// </summary>
        /// <value>
        ///     The full description.
        /// </value>
        public string FullDescription { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HighScore" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="score">The score.</param>
        /// <param name="level">The level.</param>
        public HighScore(string name, int score, int level)
        {
            this.Name = name;
            this.GameScore = score;
            this.GameLevel = level;
            this.FullDescription = $"Score: {this.GameScore}   |   Name: {this.Name}   |   Level: {this.GameLevel}";
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Sorts the description default.
        ///     Precondition: none
        ///     PostCondition: HighScores sorted by Score
        /// </summary>
        public void SortDescriptionDefault()
        {
            this.FullDescription = $"Score: {this.GameScore}   |   Name: {this.Name}   |   Level: {this.GameLevel}";
        }

        /// <summary>
        ///     Sorts the description by level.
        ///     Precondition: none
        ///     PostCondition: HighScores sorted by Level
        /// </summary>
        public void SortDescriptionByLevel()
        {
            this.FullDescription = $"Level: {this.GameLevel}   |   Score: {this.GameScore}   |   Name: {this.Name}";
        }

        /// <summary>
        ///     Sorts the name of the description by.
        ///     Precondition: none
        ///     PostCondition: HighScores sorted by Name
        /// </summary>
        public void SortDescriptionByName()
        {
            this.FullDescription = $"Name: {this.Name}   |   Score: {this.GameScore}   |   Level: {this.GameLevel}";
        }

        #endregion
    }
}