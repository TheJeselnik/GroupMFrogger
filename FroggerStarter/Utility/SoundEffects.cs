using System;
using Windows.ApplicationModel;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace FroggerStarter.Utility
{
    /// <summary>Handles the sound effects of the application.</summary>
    public static class SoundEffects
    {
        #region Data members

        private static readonly MediaPlayer Player = new MediaPlayer();

        #endregion

        #region Methods

        /// <summary>
        ///     Plays the splat sound.
        ///     Precondition: none
        ///     Postcondition: plays splat sound
        /// </summary>
        public static async void PlaySplatSound()
        {
            var folder =
                await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            var file = await folder.GetFileAsync("splat.mp3");

            Player.AutoPlay = false;
            Player.Source = MediaSource.CreateFromStorageFile(file);
            Player.Play();
        }

        /// <summary>
        ///     Plays the boundary sound.
        ///     Precondition: none
        ///     Postcondition: plays bump sound
        /// </summary>
        public static async void PlayBoundarySound()
        {
            var folder =
                await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            var file = await folder.GetFileAsync("bump.mp3");

            Player.AutoPlay = false;
            Player.Source = MediaSource.CreateFromStorageFile(file);
            Player.Play();
        }

        /// <summary>
        ///     Plays the water splash sound.
        ///     Precondition: none
        ///     Postcondition: plays splash sound
        /// </summary>
        public static async void PlayWaterSplashSound()
        {
            var folder =
                await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            var file = await folder.GetFileAsync("water_splash.mp3");

            Player.AutoPlay = false;
            Player.Source = MediaSource.CreateFromStorageFile(file);
            Player.Play();
        }

        /// <summary>
        ///     Plays the time out sound.
        ///     Precondition: none
        ///     Postcondition: plays timeout sound
        /// </summary>
        public static async void PlayTimeOutSound()
        {
            var folder =
                await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            var file = await folder.GetFileAsync("time_beep.mp3");

            Player.AutoPlay = false;
            Player.Source = MediaSource.CreateFromStorageFile(file);
            Player.Play();
        }

        /// <summary>
        ///     Plays the home landing sound.
        ///     Precondition: none
        ///     Postcondition: plays home land sound
        /// </summary>
        public static async void PlayHomeLandingSound()
        {
            var folder =
                await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            var file = await folder.GetFileAsync("home.mp3");

            Player.AutoPlay = false;
            Player.Source = MediaSource.CreateFromStorageFile(file);
            Player.Play();
        }

        /// <summary>
        ///     Plays the game over sound.
        ///     Precondition: none
        ///     Postcondition: plays a game over sound
        /// </summary>
        public static async void PlayGameOverSound()
        {
            var folder =
                await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            var file = await folder.GetFileAsync("game_over.mp3");

            Player.AutoPlay = false;
            Player.Source = MediaSource.CreateFromStorageFile(file);
            Player.Play();
        }

        /// <summary>
        ///     Plays the level complete sound.
        ///     Precondition: none
        ///     Postcondition: plays level complete sound
        /// </summary>
        public static async void PlayLevelCompleteSound()
        {
            var folder =
                await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            var file = await folder.GetFileAsync("level_complete.mp3");

            Player.AutoPlay = false;
            Player.Source = MediaSource.CreateFromStorageFile(file);
            Player.Play();
        }

        /// <summary>
        ///     Plays the power up sound.
        ///     Precondition: none
        ///     Postcondition: plays power up sound
        /// </summary>
        public static async void PlayPowerUpSound()
        {
            var folder =
                await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            var file = await folder.GetFileAsync("power_up.mp3");

            Player.AutoPlay = false;
            Player.Source = MediaSource.CreateFromStorageFile(file);
            Player.Play();
        }

        /// <summary>
        ///     Plays the title sound.
        ///     Precondition: none
        ///     Postcondition: plays title music
        /// </summary>
        public static async void PlayTitleSound()
        {
            var folder =
                await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            var file = await folder.GetFileAsync("title_music.wav");

            Player.AutoPlay = false;
            Player.Source = MediaSource.CreateFromStorageFile(file);
            Player.IsLoopingEnabled = true;
            Player.Play();
        }

        /// <summary>
        ///     Pauses the sound from player.
        ///     Precondition: none
        ///     Postcondition: pauses sound
        /// </summary>
        public static void PauseSound()
        {
            Player.IsLoopingEnabled = false;
            Player.Pause();
        }

        #endregion
    }
}