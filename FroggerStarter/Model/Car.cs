using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    /// Defines the car type of vehicle
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.Vehicle" />
    public class Car : Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Car"/> class.
        /// </summary>
        /// <param name="direction">direction the vehicle is facing</param>
        /// <param name="initialSpeed">initial speed of vehicle</param>
        public Car(Direction direction, double initialSpeed) : base(direction, initialSpeed)
        {
            Sprite = new CarSprite();
            this.SetSpeed(initialSpeed, SpeedY);

            if (direction == Direction.Right)
            {
                this.RotateSprite();
            }
        }     
        

    }
}
