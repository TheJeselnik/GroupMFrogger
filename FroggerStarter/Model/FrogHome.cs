using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    /// Defines the frog homes the frog lands in
    /// </summary>
    public class FrogHome : GameObject
    {

        /// <summary>
        /// Gets a value indicating whether this instance has frog.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has frog; otherwise, <c>false</c>.
        /// </value>
        public bool HasFrog { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrogHome"/> class.
        /// </summary>
        public FrogHome()
        {
            Sprite = new FrogHomeSprite();
        }

        /// <summary>
        /// Adds the frog to the home.
        /// </summary>
        public void AddFrog()
        {
            this.HasFrog = true;
            var oldX = this.X;
            var oldY = this.Y;
            Sprite = new FrogLandedSprite();
            this.X = oldX;
            this.Y = oldY;
        }
    }
}
