using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using FroggerStarter.Utility;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FroggerStarter.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartGamePage
    {
        #region Data members

        private readonly double applicationHeight = (double) Application.Current.Resources["AppHeight"];
        private readonly double applicationWidth = (double) Application.Current.Resources["AppWidth"];

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="StartGamePage" /> class.</summary>
        public StartGamePage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size
                {Width = this.applicationWidth, Height = this.applicationHeight};
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView()
                           .SetPreferredMinSize(new Size(this.applicationWidth, this.applicationHeight));

            SoundEffects.PlayTitleSound();
        }

        #endregion

        #region Methods

        private void StartGamedBtn_Click(object sender, RoutedEventArgs e)
        {
            SoundEffects.PauseSound();
            Frame.Navigate(typeof(GamePage), null);
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            this.clearBtn.Visibility = Visibility.Collapsed;
        }

        #endregion
    }
}