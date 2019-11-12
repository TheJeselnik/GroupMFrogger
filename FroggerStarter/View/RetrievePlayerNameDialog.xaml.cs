using Windows.UI.Xaml;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FroggerStarter.View
{
    /// <summary>Content dialog for retrieving the players name at end of game.</summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class RetrievePlayerNameDialog
    {
        #region Data members

        private readonly int playerScore;

        private readonly int playerLevel;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="RetrievePlayerNameDialog" /> class.</summary>
        /// <param name="score">The score.</param>
        /// <param name="level">The level.</param>
        public RetrievePlayerNameDialog(int score, int level)
        {
            this.InitializeComponent();

            this.scoreTextBox.Text = score.ToString();
            this.levelTextBox.Text = level.ToString();

            this.playerScore = score;
            this.playerLevel = level;
        }

        #endregion

        #region Methods

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            var name = this.nameTextBox.Text;

            this.nameTextBox.Text = name + "," + this.playerScore + "," + this.playerLevel;
            this.dialog.Hide();
        }

        #endregion
    }
}