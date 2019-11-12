using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.GameObjects
{
    /// <summary>
    ///     Defines the oil semi truck type of vehicle
    /// </summary>
    /// <seealso cref="SemiTruck" />
    public class OilSemiTruck : SemiTruck
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="OilSemiTruck" /> class.
        /// </summary>
        /// <param name="direction">direction the vehicle is facing</param>
        /// <param name="initialSpeed">initial speed of vehicle</param>
        public OilSemiTruck(Direction direction, double initialSpeed) : base(direction, initialSpeed)
        {
            Sprite = new OilSemiTruckSprite();
            SetSpeed(initialSpeed, SpeedY);

            if (direction == Direction.Right)
            {
                FlipSprite();
            }
        }

        #endregion
    }
}