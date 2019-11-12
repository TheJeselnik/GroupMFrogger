using System.Collections.Generic;
using FroggerStarter.Model.DataObjects;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.GameObjects
{
    /// <summary>
    ///     Defines the frog model
    /// </summary>
    /// <seealso cref="GameObject" />
    public class Frog : GameObject
    {
        #region Data members

        /// <summary>
        ///     The frog sprite
        /// </summary>
        public BaseSprite FrogSprite = new FrogSprite();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the death sprites.
        /// </summary>
        /// <value>
        /// The death sprites.
        /// </value>
        public IList<BaseSprite> DeathSprites { get; private set; }

        /// <summary>
        /// Gets the jump sprites.
        /// </summary>
        /// <value>
        /// The jump sprites.
        /// </value>
        public IList<BaseSprite> JumpSprites { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Frog" /> class.
        /// </summary>
        public Frog()
        {
            Sprite = this.FrogSprite;
            SetSpeed(GameSettings.DefaultFrogSpeed, GameSettings.DefaultFrogSpeed);
            this.setDeathSprites();
            this.setJumpSprites();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Sets the sprite of the player frog.
        ///     Precondition: None
        ///     Postcondition: player.Sprite = sprite
        /// </summary>
        /// <param name="sprite">The sprite.</param>
        public void SetSprite(BaseSprite sprite)
        {
            Sprite = sprite;
        }

        private void setDeathSprites()
        {
            IList<BaseSprite> deathSprites = new List<BaseSprite> {
                new FrogFirstDeathSprite(), new FrogSecondDeathSprite(), new FrogThirdDeathSprite(),
                new FrogCrossbonesSprite()
            };
            this.DeathSprites = deathSprites;
        }

        private void setJumpSprites()
        {
            IList<BaseSprite> jumpSprites = new List<BaseSprite>() {
                new FrogJumpingSprite(), new FrogSprite()
            };
            this.JumpSprites = jumpSprites;
        }

        #endregion
    }
}