namespace FroggerStarter.Model
{
    /// <summary>
    ///     Manages values of the player, like lives, score, and if the game is over.
    /// </summary>
    public class PlayerValues
    {
        #region Properties

        /// <summary>
        ///     Gets the player lives.
        /// </summary>
        /// <value>
        ///     The player lives.
        /// </value>
        public int PlayerLives { get; private set; }

        /// <summary>
        ///     Gets the player score.
        /// </summary>
        /// <value>
        ///     The player score.
        /// </value>
        public int PlayerScore { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether [game over].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [game over]; otherwise, <c>false</c>.
        /// </value>
        public bool GameOver { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerValues" /> class.
        /// </summary>
        /// <param name="lives">The lives.</param>
        /// <param name="score">The score.</param>
        public PlayerValues(int lives, int score)
        {
            this.PlayerLives = lives;
            this.PlayerScore = score;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Decreases Player Lives.
        ///     Precondition: None
        ///     Postcondition: Lives == Lives@prev - 1
        /// </summary>
        public void LoseALife()
        {
            this.PlayerLives--;
            if (this.PlayerLives <= 0)
            {
                this.GameOver = true;
            }
        }

        /// <summary>
        ///     Increases Player Score.
        ///     Precondition: None
        ///     Postcondition: Score == Score@prev + 1
        /// </summary>
        public void IncreaseScore()
        {
            this.PlayerScore++;
            if (this.PlayerScore >= 3)
            {
                this.GameOver = true;
            }
        }

        #endregion
    }
}