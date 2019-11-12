using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.GameObjects
{
    /// <summary>
    ///     Defines the bush object
    /// </summary>
    public class Bush : GameObject
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Bush" /> class.
        /// </summary>
        public Bush()
        {
            Sprite = new BushSprite();
        }

        #endregion
    }
}