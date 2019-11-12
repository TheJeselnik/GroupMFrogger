using System;

namespace FroggerStarter.Model.GameObjects
{
    /// <summary>
    ///     Defines the model for objects in water
    /// </summary>
    public abstract class WaterObject : Vehicle
    {
        #region Types and Delegates

        /// <summary>
        ///     Enum types of Water Objects
        /// </summary>
        public enum WaterObjectType
        {
            /// <summary>
            ///     The raft
            /// </summary>
            Raft,

            /// <summary>
            ///     The log
            /// </summary>
            Log
        }

        #endregion

        #region Data members

        /// <summary>
        ///     Determines if the WaterObject can be landed on
        /// </summary>
        public bool CanLandOn;

        private readonly Direction direction;
        private readonly double initialSpeed;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="WaterObject" /> class.
        /// </summary>
        /// <param name="canLandOn">if set to <c>true</c> [can land on].</param>
        /// <param name="direction">The direction</param>
        /// <param name="speed">The speed.</param>
        protected WaterObject(bool canLandOn, Direction direction, double speed) : base(direction, speed)
        {
            this.CanLandOn = canLandOn;
            this.direction = direction;
            this.initialSpeed = speed;
            SetSpeed(speed, 0.0);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves the landed frog.
        /// </summary>
        /// <param name="player">The player.</param>
        public void MoveLandedFrog(Frog player)
        {
            var oldFrogSpeedX = player.SpeedX;
            var oldFrogSpeedY = player.SpeedY;
            player.SetSpeed(this.initialSpeed, 0.0);

            switch (this.direction)
            {
                case Direction.Left:
                    player.MoveLeft();
                    break;
                case Direction.Right:
                    player.MoveRight();
                    break;
                case Direction.Up:
                    break;
                case Direction.Down:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            player.SetSpeed(oldFrogSpeedX, oldFrogSpeedY);
        }

        #endregion
    }
}