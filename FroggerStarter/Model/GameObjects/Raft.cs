using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.GameObjects
{
    /// <summary>
    ///     Defines the raft water object
    /// </summary>
    public class Raft : WaterObject
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Raft" /> class.
        /// </summary>
        /// <param name="canLandOn">if set to <c>true</c> [can land on].</param>
        /// <param name="direction">The direction</param>
        /// <param name="speed">The speed.</param>
        public Raft(bool canLandOn, Direction direction, double speed) : base(canLandOn, direction, speed)
        {
            Sprite = new RaftSprite();
        }

        #endregion
    }
}