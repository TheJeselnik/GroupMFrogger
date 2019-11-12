
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.GameObjects
{
    /// <summary>
    /// Defines the power up for adding bonus score when collected
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.GameObjects.PowerUp" />
    public class ScorePowerUp : PowerUp
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScorePowerUp"/> class.
        /// </summary>
        public ScorePowerUp()
        {
            Sprite = new ScorePowerUpSprite();
        }
    }
}
