using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.GameObjects
{
    /// <summary>
    ///     Defines the semi truck type of vehicle
    /// </summary>
    /// <seealso cref="Vehicle" />
    public class SemiTruck : Vehicle
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SemiTruck" /> class.
        /// </summary>
        /// <param name="direction">direction the vehicle is facing</param>
        /// <param name="initialSpeed">initial speed of vehicle</param>
        public SemiTruck(Direction direction, double initialSpeed) : base(direction, initialSpeed)
        {
            Sprite = new SemiTruckSprite();
            SetSpeed(initialSpeed, SpeedY);

            if (direction == Direction.Right)
            {
                FlipSprite();
            }
        }

        #endregion
    }
}