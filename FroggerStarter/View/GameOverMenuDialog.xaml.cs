using System;
using Windows.UI.Xaml;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FroggerStarter.View
{
    /// <summary>Content dialog for the game over menu.</summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class GameOverMenuDialog
    {
        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="GameOverMenuDialog" /> class.</summary>
        public GameOverMenuDialog()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        private void RestartBtn_Click(object sender, RoutedEventArgs e)
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

        #endregion
    }
}