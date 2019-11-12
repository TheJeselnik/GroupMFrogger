using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
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
    public sealed partial class GameOverMenuDialog
    {
        public GameOverMenuDialog()
        {
            this.InitializeComponent();
        }

        private async void RestartBtn_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Restart game
            
        }

        private void ViewScoreBoardBtn_Click(object sender, RoutedEventArgs e)
        {
            this.dialog.Hide();
        }

        private void EndGameBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
