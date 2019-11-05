using System;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using FroggerStarter.Controller;
using FroggerStarter.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FroggerStarter.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage
    {
        #region Data members

        private readonly double applicationHeight = (double) Application.Current.Resources["AppHeight"];
        private readonly double applicationWidth = (double) Application.Current.Resources["AppWidth"];
        private readonly GameManager gameManager;

        #endregion

        #region Constructors

        public GamePage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size
                {Width = this.applicationWidth, Height = this.applicationHeight};
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView()
                           .SetPreferredMinSize(new Size(this.applicationWidth, this.applicationHeight));

            Window.Current.CoreWindow.KeyDown += this.coreWindowOnKeyDown;
            this.gameManager = new GameManager(this.applicationHeight, this.applicationWidth);
            this.gameManager.InitializeGame(this.canvas);

            this.scoreTextBlock.Text = $"{GameSettings.InitialScore:00000}";
            this.livesRemainingTextBlock.Text = $"{GameSettings.InitialLives}";

            this.gameManager.ScoreUpdated += this.scoreOnScoreUpdated;
            this.gameManager.LivesUpdated += this.livesOnLivesUpdated;
            this.gameManager.GameOverReached += this.gameOverOnGameOverReached;
            this.gameManager.LifeTimerUpdated += this.lifeTimerOnLifeTimerUpdated;
        }

        private void lifeTimerOnLifeTimerUpdated(object sender, double timeRemaining)
        {
            var percentageValue = timeRemaining * 0.1;
            this.timeProgressRectangle.Width = percentageValue * GameSettings.TimerWidth;
        }

        private void gameOverOnGameOverReached(object sender, EventArgs e)
        {
            this.gameOverTextBlock.Visibility = Visibility.Visible;
        }

        private void scoreOnScoreUpdated(object sender, int score)
        {
            this.scoreTextBlock.Text = $"{score:00000}";
        }

        private void livesOnLivesUpdated(object sender, int lives)
        {
            this.livesRemainingTextBlock.Text = $"{lives}";
        }

        #endregion

        #region Methods

        private void coreWindowOnKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    this.gameManager.MovePlayerLeft();
                    break;
                case VirtualKey.Right:
                    this.gameManager.MovePlayerRight();
                    break;
                case VirtualKey.Up:
                    this.gameManager.MovePlayerUp();
                    break;
                case VirtualKey.Down:
                    this.gameManager.MovePlayerDown();
                    break;
            }
        }

        #endregion
    }
}