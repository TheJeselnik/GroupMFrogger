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
    public sealed partial class HighScoreBoardDialog : ContentDialog
    {
        public bool isOpenedAtStartScreen { get; set; }

        public HighScoreBoardDialog()
        {
            this.InitializeComponent();
        }

        private async void CloseBtn_Click(object sender, RoutedEventArgs e)
        {

            if (this.isOpenedAtStartScreen)
            {
                this.dialog.Hide();
            }
            else
            {
                this.dialog.Hide();
                var dialog = new GameOverMenuDialog();
                await dialog.ShowAsync();
            }
           
        }
    }
}
