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
using FroggerStarter.ViewModel;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FroggerStarter.View
{
    public sealed partial class RetrievePlayerNameDialog
    {
        private int playerScore;

        private int playerLevel;

        public RetrievePlayerNameDialog(int score, int level)
        {
            this.InitializeComponent();

            this.scoreTextBox.Text = score.ToString();
            this.levelTextBox.Text = level.ToString();

            this.playerScore = score;
            this.playerLevel = level;
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            var name = this.nameTextBox.Text;

            this.nameTextBox.Text = name + "," + this.playerScore + "," + this.playerLevel;
            this.dialog.Hide();
        }
    }
}
