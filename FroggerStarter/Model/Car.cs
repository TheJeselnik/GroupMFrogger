using FroggerStarter.View.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Media;

namespace FroggerStarter.Model
{
    public class Car : Vehicle
    {
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
