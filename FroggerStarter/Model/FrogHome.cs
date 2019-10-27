

using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    /// Defines the frog homes the frog lands in
    /// </summary>
    public class FrogHome : GameObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FrogHome"/> class.
        /// </summary>
        public FrogHome()
        {
            Sprite = new FrogHomeLandingSpotSprite();
        }
    }
}
