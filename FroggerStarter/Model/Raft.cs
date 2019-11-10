
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    /// Defines the raft water vehicle
    /// </summary>
    public class Raft : Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Raft"/> class.
        /// </summary>
        /// <param name="direction">direction the vehicle is facing</param>
        /// <param name="initialSpeed">initial speed of vehicle</param>
        public Raft(Direction direction, double initialSpeed) : base(direction, initialSpeed)
        {
            Sprite = new RaftSprite();
            this.SetSpeed(initialSpeed, SpeedY);

            if (direction == Direction.Right)
            {
                this.RotateSprite();
            }
        }
    }
}
