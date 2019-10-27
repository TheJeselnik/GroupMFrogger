namespace FroggerStarter.Model
{
    /// <summary>
    ///     Manages values of the player, like lives, score, and if the game is over.
    /// </summary>
    public class PlayerValues
    {

        private const int ScoreLimit = 3;

        #region Properties

        /// <summary>
        ///     Gets the player lives.
        /// </summary>
        /// <value>
        ///     The player lives.
        /// </value>
        public int Lives { get; private set; }

        /// <summary>
        ///     Gets the player score.
        /// </summary>
        /// <value>
        ///     The player score.
        /// </value>
        public int Score { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether [game over].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [game over]; otherwise, <c>false</c>.
        /// </value>
        public bool GameOver { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [frog dying].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [frog dying]; otherwise, <c>false</c>.
        /// </value>
        public bool FrogDying { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerValues" /> class.
        /// </summary>
        /// <param name="lives">The lives.</param>
        /// <param name="score">The score.</param>
        public PlayerValues(int lives, int score)
        {
            this.Lives = lives;
            this.Score = score;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Decreases Lives.
        ///     Precondition: None
        ///     Postcondition: Lives == Lives@prev - 1
        /// </summary>
        public void LoseALife()
        {
            this.Lives--;
            this.FrogDying = true;
            if (this.Lives <= 0)
            {
                this.GameOver = true;
            }
        }

        /// <summary>
        ///     Increases Score.
        ///     Precondition: None
        ///     Postcondition: Score == Score@prev + 1
        /// </summary>
        public void IncreaseScore()
        {
            this.Score++;
            if (this.Score >= ScoreLimit)
            {
                this.GameOver = true;
            }
        }

        /// <summary>
        /// Revives the frog.
        ///     Precondition: FrogDying = true
        ///     Postcondition: FrogDying = false;
        /// </summary>
        public void ReviveFrog()
        {
            this.FrogDying = false;
        }

        #endregion
    }
}