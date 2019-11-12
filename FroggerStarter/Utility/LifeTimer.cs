using FroggerStarter.Model.DataObjects;

namespace FroggerStarter.Utility
{
    /// <summary>
    ///     Defines the timer for the player/frog's life
    /// </summary>
    public class LifeTimer
    {
        #region Properties

        /// <summary>
        ///     Gets the time remaining.
        /// </summary>
        /// <value>
        ///     The time remaining.
        /// </value>
        public double TimeRemaining { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LifeTimer" /> class.
        /// </summary>
        public LifeTimer()
        {
            this.ResetTimeRemaining();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Decreases the time remaining.
        ///     Precondition: TimeRemaining != null
        ///     Postcondition: TimeRemaining = TimeRemaining@prev - GameSettings.TimerMilliseconds
        /// </summary>
        public void DecreaseTimeRemaining()
        {
            this.TimeRemaining -= millisecondsToSeconds(GameSettings.TimerMilliseconds);
            if (this.TimeRemaining < 0.0)
            {
                this.TimeRemaining = 0.0;
            }
        }

        /// <summary>
        ///     Adds to the time remaining.
        ///     Precondition: TimeRemaining != null
        ///     Postcondition: TimeRemaining = TimeRemaining@prev + GameSettings.BonusTime
        /// </summary>
        public void AddTime()
        {
            this.TimeRemaining += GameSettings.BonusTimeRewardSeconds;
            if (this.TimeRemaining > GameSettings.TimeLimitSeconds)
            {
                this.TimeRemaining = GameSettings.TimeLimitSeconds;
            }
        }

        /// <summary>
        ///     Resets the time remaining.
        ///     Precondition: none
        ///     Postcondition: TimeRemaining = GameSettings.TimeLimitSeconds
        /// </summary>
        public void ResetTimeRemaining()
        {
            this.TimeRemaining = GameSettings.TimeLimitSeconds;
        }

        private static double millisecondsToSeconds(double milliseconds)
        {
            return milliseconds * 0.001;
        }

        #endregion
    }
}