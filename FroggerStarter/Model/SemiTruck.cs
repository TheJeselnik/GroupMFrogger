using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    /// Defines the semi truck type of vehicle
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.Vehicle" />
    public class SemiTruck : Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SemiTruck"/> class.
        /// </summary>
        /// <param name="direction">direction the vehicle is facing</param>
        /// <param name="initialSpeed">initial speed of vehicle</param>
        public SemiTruck(Direction direction, double initialSpeed) : base(direction, initialSpeed)
        {
            Sprite = new SemiTruckSprite();
            this.SetSpeed(initialSpeed, SpeedY);

            if (direction == Direction.Right)
            {
                this.RotateSprite();
            }
        }
    }
}
