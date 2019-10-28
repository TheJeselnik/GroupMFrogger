using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    /// <summary>
    /// Defines a shoulder of the road, which may contain FrogHomes
    /// </summary>
    public class Shoulder
    {
        private readonly double frogWidth;

        public Shoulder(double frogWidth)
        {
            this.frogWidth = frogWidth;
        }

        private void calculateFrogHomePlacement()
        {
            var numberOfVerticalLanes = GameSettings.RoadWidth / this.frogWidth;
            var emptyVerticalLanes = numberOfVerticalLanes - GameSettings.NumberOfFrogHomes;
        }
    }
}
