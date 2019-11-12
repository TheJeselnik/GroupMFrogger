using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using FroggerStarter.Model;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FroggerStarter.View
{
    public sealed partial class HighScoreBoardDialog
    {
        public bool IsOpenedAtStartScreen { get; set; }

        public HighScoreBoardDialog()
        {
            this.InitializeComponent();

            this.levelSortBtn.IsEnabled = false;
            this.nameSortBtn.IsEnabled = false;
            this.scoreSortBtn.IsEnabled = false;
        }

        private async void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsOpenedAtStartScreen)
            {
                this.dialog.Hide();
            }
            else
            {
                this.dialog.Hide();
                var menuDialog = new GameOverMenuDialog();
                await menuDialog.ShowAsync();
            }
        }

        private void DisplayScoresBtn_Click(object sender, RoutedEventArgs e)
        {
            this.levelSortBtn.IsEnabled = true;
            this.nameSortBtn.IsEnabled = true;
            this.scoreSortBtn.IsEnabled = true;
            this.displayBtn.IsEnabled = false;
        }
    }
}
