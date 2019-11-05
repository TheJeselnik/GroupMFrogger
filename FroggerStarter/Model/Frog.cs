using System.Collections.Generic;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Defines the frog model
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public class Frog : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 50;
        private const int SpeedYDirection = 50;

        /// <summary>
        ///     The frog sprite
        /// </summary>
        public BaseSprite FrogSprite = new FrogSprite();

        #endregion

        #region Properties

        /// <summary>
        ///     The death sprites
        /// </summary>
        public IList<BaseSprite> DeathSprites { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Frog" /> class.
        /// </summary>
        public Frog()
        {
            Sprite = this.FrogSprite;
            SetSpeed(SpeedXDirection, SpeedYDirection);
            this.setDeathSprites();
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

        #endregion
    }
}