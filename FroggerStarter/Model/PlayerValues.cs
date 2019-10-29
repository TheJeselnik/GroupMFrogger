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
        ///     Gets a value indicating whether [frog dying].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [frog dying]; otherwise, <c>false</c>.
        /// </value>
        public bool FrogDying { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerValues" /> class.
        /// </summary>
        public PlayerValues()
        {
            this.Lives = GameSettings.InitialLives;
            this.Score = GameSettings.InitialScore;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Decreases Lives.
        ///     Precondition: Lives >= 1
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
        ///     Postcondition: Score += timeRemaining * remainingLives * 1000
        /// </summary>
        public void IncreaseScore(double timeRemaining)
        {
            var newScore = timeRemaining * this.Lives * GameSettings.ScoreMultiplier;
            this.Score += (int) newScore;
        }

        /// <summary>
        ///     Checks for game over from score.
        ///     Precondition: GameOver = false
        ///     Postcondition: GameOver = true || GameOver = false
        /// </summary>
        /// <param name="allFrogHomesFilled">if set to <c>true</c> [all frog homes filled].</param>
        public void CheckForGameOverFromScore(bool allFrogHomesFilled)
        {
            this.GameOver = allFrogHomesFilled;
        }

        /// <summary>
        ///     Revives the frog.
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