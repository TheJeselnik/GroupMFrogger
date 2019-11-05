using System;
using Windows.UI.Xaml;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Handles animations in the game.
    /// </summary>
    public class AnimationManager
    {
        #region Data members

        private int frogDeathTicks;
        private Frog player;
        private DispatcherTimer timer;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a value indicating whether [frog dying].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [frog dying]; otherwise, <c>false</c>.
        /// </value>
        public bool FrogDying { get; private set; }

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
            this.player = frog;
            this.FrogDying = true;
            this.frogDeathTicks = 0;
            this.animateFrogDeathSprites();
            this.setupAnimationTimer();
            this.timer.Start();
        }

        private void setupAnimationTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.timerOnTick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 750);
            this.timer.Start();
        }

        private void timerOnTick(object sender, object e)
        {
            if (this.FrogDying)
            {
                this.animateFrogDeathSprites();
            }
        }

        private void animateFrogDeathSprites()
        {
            if (this.frogDeathTicks == this.player.DeathSprites.Count)
            {
                this.endFrogAnimation();
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

        private void endFrogAnimation()
        {
            this.FrogDying = false;
            this.timer.Stop();
        }

        #endregion
    }
}