using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    /// Defines the oil semi truck type of vehicle
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.SemiTruck" />
    public class OilSemiTruck : SemiTruck
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OilSemiTruck"/> class.
        /// </summary>
        /// <param name="direction">direction the vehicle is facing</param>
        /// <param name="initialSpeed">initial speed of vehicle</param>
        public OilSemiTruck(Direction direction, double initialSpeed) : base(direction, initialSpeed)
        {
            Sprite = new OilSemiTruckSprite();
            this.SetSpeed(initialSpeed, SpeedY);

            if (direction == Direction.Right)
            {
                this.RotateSprite();
            }
        }
    }
}
