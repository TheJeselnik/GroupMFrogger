using System;
using Windows.UI.Xaml;
using FroggerStarter.Model.GameObjects;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Handles animations in the game.
    /// </summary>
    public class AnimationManager
    {
        #region Data members

        private int frogDeathTicks;
        private int frogJumpTicks;
        private Frog player;
        private DispatcherTimer deathTimer;
        private DispatcherTimer jumpTimer;
        private GameObject.Direction direction;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a value indicating whether [frog dying].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [frog dying]; otherwise, <c>false</c>.
        /// </value>
        public bool FrogDying { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether [frog jumping].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [frog jumping]; otherwise, <c>false</c>.
        /// </value>
        public bool FrogJumping { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Animates the frog death.
        ///     Precondition: none
        ///     Postcondition: Frog animations queued to new timer
        /// </summary>
        /// <param name="frog">The frog.</param>
        public void AnimateFrogDeath(Frog frog)
        {
            if (this.FrogJumping)
            {
                this.FrogJumping = false;
                this.player.ResetSprite();
            }

            this.player = frog;
            this.FrogDying = true;
            this.frogDeathTicks = 0;
            this.animateFrogDeathSprites();
            this.setupDeathAnimationTimer();
            this.deathTimer.Start();
        }

        /// <summary>
        ///     Animates the frog jump.
        ///     Precondition: none
        ///     Postcondition: Frog animation queued to new timer
        /// </summary>
        /// <param name="frog">The frog.</param>
        /// <param name="frogDirection">The frog direction.</param>
        public void AnimateFrogJump(Frog frog, GameObject.Direction frogDirection)
        {
            this.player = frog;
            this.direction = frogDirection;
            this.FrogJumping = true;
            this.frogJumpTicks = 0;
            this.animateFrogJumpSprite();
            this.setupJumpAnimationTimer();
            this.jumpTimer.Start();
        }

        private void setupDeathAnimationTimer()
        {
            this.deathTimer = new DispatcherTimer();
            this.deathTimer.Tick += this.deathTimerOnTick;
            this.deathTimer.Interval = new TimeSpan(0, 0, 0, 0, 750);
            this.deathTimer.Start();
        }

        private void setupJumpAnimationTimer()
        {
            this.jumpTimer = new DispatcherTimer();
            this.jumpTimer.Tick += this.jumpTimerOnTick;
            this.jumpTimer.Interval = new TimeSpan(0, 0, 0, 0, 30);
            this.jumpTimer.Start();
        }

        private void jumpTimerOnTick(object sender, object e)
        {
            if (this.FrogJumping)
            {
                this.animateFrogJumpSprite();
            }
        }

        private void deathTimerOnTick(object sender, object e)
        {
            if (this.FrogDying)
            {
                this.animateFrogDeathSprites();
            }
        }

        private void animateFrogJumpSprite()
        {
            if (this.frogJumpTicks >= 1)
            {
                this.endJumpAnimation();
            }

            var oldX = this.player.X;
            var oldY = this.player.Y;

            this.player.Sprite.Visibility = Visibility.Collapsed;
            this.player.SetSprite(this.player.JumpSprites[this.frogJumpTicks]);
            this.player.RotateSprite(this.direction);
            this.player.Sprite.Visibility = Visibility.Visible;

            this.player.X = oldX;
            this.player.Y = oldY;

            this.frogJumpTicks++;
        }

        private void animateFrogDeathSprites()
        {
            if (this.frogDeathTicks == this.player.DeathSprites.Count)
            {
                this.endDeathAnimation();
            }
            else
            {
                var oldX = this.player.X;
                var oldY = this.player.Y;

                this.player.Sprite.Visibility = Visibility.Collapsed;

                this.player.SetSprite(this.player.DeathSprites[this.frogDeathTicks]);
                this.player.Sprite.Visibility = Visibility.Visible;

                this.player.X = oldX;
                this.player.Y = oldY;

                this.frogDeathTicks++;
            }
        }

        private void endJumpAnimation()
        {
            this.FrogJumping = false;
            this.jumpTimer.Stop();
        }

        private void endDeathAnimation()
        {
            this.FrogDying = false;
            this.deathTimer.Stop();
        }

        #endregion
    }
}