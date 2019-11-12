using System.Collections.Generic;
using FroggerStarter.Model.GameObjects;

namespace FroggerStarter.Controller
{
    /// <summary>
    /// Defines when power ups are placed
    /// </summary>
    public class PowerUpManager
    {

        private IList<PowerUp> powerUpsInPlay;

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerUpManager"/> class.
        /// </summary>
        public PowerUpManager()
        {
            this.powerUpsInPlay = new List<PowerUp>();
        }

    }
}
