using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.GameObjects
{
    /// <summary>
    ///     Defines the log water object
    /// </summary>
    /// <seealso cref="WaterObject" />
    public class Log : WaterObject
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Log" /> class.
        /// </summary>
        /// <param name="canLandOn">if set to <c>true</c> [can land on].</param>
        /// <param name="speed">The speed.</param>
        public Log(bool canLandOn, Direction direction, double speed) : base(canLandOn, direction, speed)
        {
            Sprite = new LogSprite();
        }

        #endregion
    }
}