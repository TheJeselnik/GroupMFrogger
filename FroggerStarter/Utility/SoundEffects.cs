using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace FroggerStarter.Utility
{
    public static class SoundEffects
    {
        private static readonly MediaPlayer player = new MediaPlayer();

        public static async void PlayDeathSound()
        {
            var folder =
                await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            var file = await folder.GetFileAsync("splat.mp3");

            player.AutoPlay = false;
            player.Source = MediaSource.CreateFromStorageFile(file);
            player.Play();
        }

        public static async void PlayBoundarySound()
        {
            var folder =
                await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            var file = await folder.GetFileAsync("bump.mp3");

            player.AutoPlay = false;
            player.Source = MediaSource.CreateFromStorageFile(file);
            player.Play();
        }

        public static async void PlayWaterSplashSound()
        {
            var folder =
                await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            var file = await folder.GetFileAsync("water_splash.mp3");

            player.AutoPlay = false;
            player.Source = MediaSource.CreateFromStorageFile(file);
            player.Play();
        }

        public static async void PlayTimeOutSound()
        {
            var folder =
                await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            var file = await folder.GetFileAsync("time_beep.mp3");

            player.AutoPlay = false;
            player.Source = MediaSource.CreateFromStorageFile(file);
            player.Play();
        }

        public static async void PlayHomeLandingSound()
        {
            var folder =
                await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            var file = await folder.GetFileAsync("home.mp3");

            player.AutoPlay = false;
            player.Source = MediaSource.CreateFromStorageFile(file);
            player.Play();
        }

        public static async void PlayGameOverSound()
        {
            var folder =
                await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            var file = await folder.GetFileAsync("game_over.mp3");

            player.AutoPlay = false;
            player.Source = MediaSource.CreateFromStorageFile(file);
            player.Play();
        }


        public static async void PlayTitleSound()
        {
            var folder =
                await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            var file = await folder.GetFileAsync("title_music.wav");

            player.AutoPlay = false;
            player.Source = MediaSource.CreateFromStorageFile(file);
            player.Play();
        }

        public static void PauseSound()
        {
            player.Pause();
        }
    }
}
