using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    public class OilSemiTruck : SemiTruck
    {
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
