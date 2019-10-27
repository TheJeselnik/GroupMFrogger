using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using FroggerStarter.Model;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Controller
{
    /// <summary>
    /// Handles animations in the game.
    /// </summary>
    public class AnimationManager
    {
        private Frog player;
        private int frogDeathTicks;

        private DispatcherTimer timer;

        public bool FrogDying { get; private set; }

        public void AnimateFrogDeath(Frog frog)
        {
            this.player = frog;
            this.FrogDying = true;
            this.frogDeathTicks = 0;
            this.setupGameTimer();
            this.timer.Start();
        }

        private void setupGameTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.timerOnTick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 1);
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
                return;
            }

            var oldX = this.player.X;
            var oldY = this.player.Y;
            this.player.Sprite.Visibility = Visibility.Collapsed;
            this.player.SetSprite(this.player.DeathSprites[this.frogDeathTicks]);
            this.player.Sprite.Visibility = Visibility.Visible;
            this.player.X = oldX;
            this.player.Y = oldY;
            this.frogDeathTicks++;
        }

        private void endFrogAnimation()
        {
            this.FrogDying = false;
            this.player.Sprite.Visibility = Visibility.Collapsed;
            this.player.SetSprite(this.player.FrogSprite);
            this.player.Sprite.Visibility = Visibility.Visible;
            this.timer.Stop();
        }
    }
}
