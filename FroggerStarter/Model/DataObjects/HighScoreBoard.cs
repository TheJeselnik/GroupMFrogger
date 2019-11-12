using System.Collections.Generic;

namespace FroggerStarter.Model.DataObjects
{
    /// <summary>Handles a collection of high scores</summary>
    public class HighScoreBoard
    {
        #region Properties

        /// <summary>Gets the scores.</summary>
        /// <value>The scores.</value>
        public IList<HighScore> Scores { get; }

        /// <summary>Gets the count.</summary>
        /// <value>The count.</value>
        public int Count => this.Scores.Count;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="HighScoreBoard" /> class.</summary>
        public HighScoreBoard()
        {
            this.Scores = new List<HighScore>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds the specified score.
        ///     Precondition: none
        ///     Postcondition: adds a new high score to the board.
        /// </summary>
        /// <param name="score">The score.</param>
        public void Add(HighScore score)
        {
            this.Scores.Add(score);
        }

        #endregion
    }
}