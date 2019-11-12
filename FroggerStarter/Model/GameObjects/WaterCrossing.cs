using FroggerStarter.Model.DataObjects;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.GameObjects
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
            Sprite.Width = GameSettings.RoadWidth;
            Sprite.Height = GameSettings.LaneHeight;
        }

        #endregion
    }
}