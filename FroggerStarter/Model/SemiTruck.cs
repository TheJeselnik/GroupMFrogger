using FroggerStarter.View.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    public class SemiTruck : Vehicle
    {
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
