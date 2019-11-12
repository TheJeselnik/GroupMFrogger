using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.GameObjects
{
    /// <summary>
    ///     Defines the car type of vehicle
    /// </summary>
    /// <seealso cref="Vehicle" />
    public class Car : Vehicle
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Car" /> class.
        /// </summary>
        /// <param name="direction">direction the vehicle is facing</param>
        /// <param name="initialSpeed">initial speed of vehicle</param>
        public Car(Direction direction, double initialSpeed) : base(direction, initialSpeed)
        {
            Sprite = new CarSprite();
            SetSpeed(initialSpeed, SpeedY);

            if (direction == Direction.Right)
            {
                FlipSprite();
            }
        }

        #endregion
    }
}