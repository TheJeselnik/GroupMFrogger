using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.GameObjects
{
    /// <summary>
    ///     Defines the power up for adding bonus time when collected
    /// </summary>
    public class TimePowerUp : PowerUp
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TimePowerUp" /> class.
        /// </summary>
        public TimePowerUp()
        {
            Sprite = new TimePowerUpSprite();
        }

        #endregion
    }
}