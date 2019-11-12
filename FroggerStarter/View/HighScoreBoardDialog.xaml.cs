using System;
using Windows.UI.Xaml;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FroggerStarter.View
{
    /// <summary>Content dialog for showing the high score board.</summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class HighScoreBoardDialog
    {
        #region Properties

        /// <summary>Gets or sets a value indicating whether this instance is opened at start screen.</summary>
        /// <value>
        ///     <c>true</c> if this instance is opened at start screen; otherwise, <c>false</c>.
        /// </value>
        public bool IsOpenedAtStartScreen { get; set; }

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="HighScoreBoardDialog" /> class.</summary>
        public HighScoreBoardDialog()
        {
            this.InitializeComponent();

            this.levelSortBtn.IsEnabled = false;
            this.nameSortBtn.IsEnabled = false;
            this.scoreSortBtn.IsEnabled = false;
        }

        #endregion

        #region Methods

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

        #endregion
    }
}