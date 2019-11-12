using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.GameObjects
{
    /// <summary>
    /// Defines the power up for adding bonus time when collected
    /// </summary>
    public class BonusTimePowerUp : PowerUp
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BonusTimePowerUp"/> class.
        /// </summary>
        public BonusTimePowerUp()
        {
            this.Sprite = new TimePowerUpSprite();
        }
    }
}
