

using FroggerStarter.Model;

namespace FroggerStarter.Utility
{
    /// <summary>
    /// Defines the timer for the player/frog's life
    /// </summary>
    public class LifeTimer
    {

        /// <summary>
        /// Gets the time remaining.
        /// </summary>
        /// <value>
        /// The time remaining.
        /// </value>
        public double TimeRemaining { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LifeTimer"/> class.
        /// </summary>
        public LifeTimer()
        {
            this.ResetTimeRemaining();
        }

        /// <summary>
        /// Updates the timer for the frog's remaining time.
        /// </summary>
        public void UpdateTimer()
        {
            this.TimeRemaining -= millisecondsToSeconds(GameSettings.TimerMilliseconds);
            if (this.TimeRemaining < 0.0)
            {
                this.TimeRemaining = 0.0;
            }
        }

        /// <summary>
        /// Resets the time remaining.
        /// </summary>
        public void ResetTimeRemaining()
        {
            this.TimeRemaining = GameSettings.TimeLimitSeconds;
        }

        private static double millisecondsToSeconds(double milliseconds)
        {
            return milliseconds * 0.001;
        }
    }
}
