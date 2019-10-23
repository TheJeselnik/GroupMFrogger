using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages all aspects of the game play including moving the player,
    ///     the vehicles as well as lives and score.
    /// </summary>
    public class GameManager
    {
        #region Types and Delegates

        public delegate void GameOverHandler();

        public delegate void LivesHandler(int lives);

        public delegate void ScoreHandler(int score);

        #endregion

        #region Data members

        /// <summary>
        ///     The lane height
        /// </summary>
        public const double LaneHeight = 50;

        private const int BottomLaneOffset = 5;
        private const int InitialLives = 3;
        private const int InitialScore = 0;
        private readonly double backgroundHeight;
        private readonly double backgroundWidth;
        private Canvas gameCanvas;
        private Frog player;
        private PlayerValues playerValues;
        private DispatcherTimer timer;
        private RoadManager roadManager;
        private CollisionDetector collisionDetector;
        private PlayerMovementManager playerMovementManager;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the y coordinate of the top shoulder
        /// </summary>
        /// <value>
        ///     The y coordinate of the top shoulder
        /// </value>
        public double TopShoulderY => this.roadManager.TopShoulderY;

        /// <summary>
        ///     Gets the player lives.
        /// </summary>
        /// <value>
        ///     The player lives.
        /// </value>
        public int Lives => this.playerValues.Lives;

        /// <summary>
        ///     Gets the player score.
        /// </summary>
        /// <value>
        ///     The player score.
        /// </value>
        public int Score => this.playerValues.Score;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        /// </summary>
        /// <param name="backgroundHeight">Height of the background.</param>
        /// <param name="backgroundWidth">Width of the background.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     backgroundHeight &lt;= 0
        ///     or
        ///     backgroundWidth &lt;= 0
        /// </exception>
        public GameManager(double backgroundHeight, double backgroundWidth)
        {
            if (backgroundHeight <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundHeight));
            }

            if (backgroundWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundWidth));
            }

            this.backgroundHeight = backgroundHeight;
            this.backgroundWidth = backgroundWidth;

            this.setupGameTimer();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Occurs when [player score updated].
        /// </summary>
        public event ScoreHandler ScoreUpdated;

        /// <summary>
        ///     Occurs when [player lives updated].
        /// </summary>
        public event LivesHandler LivesUpdated;

        /// <summary>
        ///     Occurs when [game over updated].
        /// </summary>
        public event GameOverHandler GameOverUpdated;

        private void setupGameTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.timerOnTick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 15);
            this.timer.Start();
        }

        /// <summary>
        ///     Initializes the game working with appropriate classes to play frog
        ///     and vehicle on game screen.
        ///     Precondition: background != null
        ///     Postcondition: Game is initialized and ready for play.
        /// </summary>
        /// <param name="gamePage">The game page.</param>
        /// <exception cref="ArgumentNullException">gameCanvas</exception>
        public void InitializeGame(Canvas gamePage)
        {
            this.gameCanvas = gamePage ?? throw new ArgumentNullException(nameof(gamePage));
            this.createAndPlacePlayer();
            this.playerValues = new PlayerValues(InitialLives, InitialScore);
            this.roadManager =
                new RoadManager(this.backgroundHeight, this.backgroundWidth, LaneHeight, BottomLaneOffset);
            this.collisionDetector = new CollisionDetector();
            this.createAndPlaceVehicles();
            this.playerMovementManager = new PlayerMovementManager(this.player,
                this.backgroundHeight, this.backgroundWidth, this.TopShoulderY, BottomLaneOffset);
        }

        private void createAndPlaceVehicles()
        {
            foreach (var vehicle in this.roadManager.AllVehicles)
            {
                this.gameCanvas.Children.Add(vehicle.Sprite);
            }
        }

        private void createAndPlacePlayer()
        {
            this.player = new Frog();
            this.gameCanvas.Children.Add(this.player.Sprite);
            this.setPlayerToCenterOfBottomLane();
        }

        private void setPlayerToCenterOfBottomLane()
        {
            this.player.X = this.backgroundWidth / 2 - this.player.Width / 2;
            this.player.Y = this.backgroundHeight - this.player.Height - BottomLaneOffset;
        }

        /// <summary>
        ///     Moves the player left.
        ///     Precondition: None
        ///     Postcondition: player.X = player.X@prev - player.Width
        /// </summary>
        public void MovePlayerLeft()
        {
            this.playerMovementManager.MovePlayerLeft();
        }

        /// <summary>
        ///     Moves the player right.
        ///     Precondition: None
        ///     Postcondition: player.X = player.X@prev + player.Width
        /// </summary>
        public void MovePlayerRight()
        {
            this.playerMovementManager.MovePlayerRight();
        }

        /// <summary>
        ///     Moves the player up.
        ///     Precondition: None
        ///     Postcondition: player.Y = player.Y@prev - player.Height
        /// </summary>
        public void MovePlayerUp()
        {
            this.playerMovementManager.MovePlayerUp();
            if (this.player.Y <= this.TopShoulderY && !this.playerValues.GameOver)
            {
                this.playerScores();
            }
        }

        /// <summary>
        ///     Moves the player down.
        ///     Precondition: None
        ///     Postcondition: player.Y = player.Y@prev + player.Height
        /// </summary>
        public void MovePlayerDown()
        {
            this.playerMovementManager.MovePlayerDown();
        }

        private void timerOnTick(object sender, object e)
        {
            this.roadManager.MoveVehiclesInEachLane();
            this.checkEachVehicleForCollision();
        }

        private void checkEachVehicleForCollision()
        {
            foreach (var currVehicle in this.roadManager.AllVehicles)
            {
                if (this.collisionDetector.IsCollisionBetween(currVehicle, this.player))
                {
                    this.playerLosesLife();
                    this.roadManager.ResetSpeed();
                }
            }
        }

        private void playerLosesLife()
        {
            this.playerValues.LoseALife();
            this.onPlayerLivesUpdated();
            this.resetFrogIfGameIsNotOver();
        }

        private void playerScores()
        {
            this.playerValues.IncreaseScore();
            this.onPlayerScoreUpdated();
            this.resetFrogIfGameIsNotOver();
        }

        private void resetFrogIfGameIsNotOver()
        {
            if (this.playerValues.GameOver)
            {
                this.gameOver();
            }
            else
            {
                this.setPlayerToCenterOfBottomLane();
            }
        }

        private void gameOver()
        {
            this.timer.Stop();
            this.onGameOver();
            this.playerMovementManager.CanMove = false;
        }

        private void onPlayerScoreUpdated()
        {
            this.ScoreUpdated?.Invoke(this.Score);
        }

        private void onPlayerLivesUpdated()
        {
            this.LivesUpdated?.Invoke(this.Lives);
        }

        private void onGameOver()
        {
            this.GameOverUpdated?.Invoke();
        }

        #endregion
    }
}