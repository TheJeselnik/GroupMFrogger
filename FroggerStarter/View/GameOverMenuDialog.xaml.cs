using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FroggerStarter.View
{
    public enum MyResult
    {
        Restart,
        ViewScoreBoard,
        EndGame
    }

    public sealed partial class GameOverMenuDialog : ContentDialog
    {
        public GameOverMenuDialog()
        {
            this.InitializeComponent();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: handle restarting app
        }

        private async void ViewScoreBoardButton_Click(object sender, RoutedEventArgs e)
        {
            this.dialog.Hide();
            var highScoreDialog = new HighScoreBoardDialog();
            await highScoreDialog.ShowAsync();
        }

        private void EndGameButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: handle closing app
        }
    }
}
