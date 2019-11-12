using Windows.UI.Xaml;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FroggerStarter.View
{
    /// <summary>Message dialog for when scoreboard is cleared.</summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class ClearedBoardMessageDialog
    {
        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="ClearedBoardMessageDialog" /> class.</summary>
        public ClearedBoardMessageDialog()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.dialog.Hide();
        }

        #endregion
    }
}