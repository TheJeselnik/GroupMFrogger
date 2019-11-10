using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Defines the water crossing
    /// </summary>
    public class WaterCrossing : GameObject
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="WaterCrossing" /> class.
        /// </summary>
        public WaterCrossing()
        {
            Sprite = new WaterCrossingSprite();
        }

        #endregion
    }
}