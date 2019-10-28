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

        private readonly double backgroundHeight;
        private readonly double backgroundWidth;
        private Canvas gameCanvas;
        private Frog player;
        private PlayerValues playerValues;
        private DispatcherTimer timer;
        private RoadManager roadManager;
        private CollisionDetector collisionDetector;
        private PlayerMovementManager playerMovementManager;
        private AnimationManager animationManager;
        private Shoulder topShoulder;

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
            GameSettings.RoadHeight = this.backgroundHeight;
            GameSettings.RoadWidth = this.backgroundWidth;
            this.createAndPlacePlayer();
            this.createFrogAnimationSprites();
            this.playerValues = new PlayerValues();
            this.roadManager = new RoadManager();
            this.collisionDetector = new CollisionDetector();
            this.animationManager = new AnimationManager();
            this.topShoulder = new Shoulder(this.player.Width);
            this.addVehiclesToCanvas();
            this.addFrogHomesToCanvas();
            this.playerMovementManager = new PlayerMovementManager(this.player);
        }

        private void addVehiclesToCanvas()
        {
            foreach (var vehicle in this.roadManager.AllVehicles)
            {
                this.gameCanvas.Children.Add(vehicle.Sprite);
            }
        }

        private void addFrogHomesToCanvas()
        {
            foreach (var frogHome in this.topShoulder.FrogHomes)
            {
                this.gameCanvas.Children.Add(frogHome.Sprite);
            }
        }

        private void createAndPlacePlayer()
        {
            this.player = new Frog();
            this.gameCanvas.Children.Add(this.player.Sprite);
            this.setPlayerToCenterOfBottomLane();
        }
        private void createFrogAnimationSprites()
        {
            foreach (var currDeathSprite in this.player.DeathSprites)
            {
                currDeathSprite.Visibility = Visibility.Collapsed;
                this.gameCanvas.Children.Add(currDeathSprite);
            }
        }

        private void setPlayerToCenterOfBottomLane()
        {
            this.player.X = this.backgroundWidth / 2 - this.player.Width / 2;
            this.player.Y = this.backgroundHeight - this.player.Height - GameSettings.RoadOffsetHeight;
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
            this.roadManager.MoveVehicles();
            this.checkPlayerCollisionWithVehicles();
            this.checkPlayerCollisionWithFrogHomes();

            this.checkIfFrogIsDoneDying();
        }

        private void checkIfFrogIsDoneDying()
        {
            if (this.playerValues.FrogDying && !this.animationManager.FrogDying)
            {
                this.resetFrogIfGameIsNotOver();
            }
        }

        private void checkPlayerCollisionWithVehicles()
        {
            foreach (var currVehicle in this.roadManager.AllVehicles)
            {
                if (this.collisionDetector.IsCollisionBetween(currVehicle, this.player) && !this.playerValues.FrogDying)
                {
                    this.playerLosesLife();
                }
            }
        }

        private void checkPlayerCollisionWithFrogHomes()
        {
            foreach (var currFrogHome in this.topShoulder.FrogHomes)
            {
                if (this.collisionDetector.IsCollisionBetween(currFrogHome, this.player))
                {
                    this.addFrogToFrogHome(currFrogHome);
                }
            }
        }

        private void addFrogToFrogHome(FrogHome frogHome)
        {
            frogHome.AddFrog();
            this.gameCanvas.Children.Add(frogHome.Sprite);
            this.playerScores();
        }

        private void playerLosesLife()
        {
            this.playerMovementManager.CanMove = false;
            this.playerValues.LoseALife();
            this.animateFrogDeath();
            this.onPlayerLivesUpdated();
        }

        private void animateFrogDeath()
        {
            this.animationManager.AnimateFrogDeath(this.player);
        }

        private void playerScores()
        {
            this.playerValues.IncreaseScore();
            this.onPlayerScoreUpdated();
            this.playerMovementManager.CanMove = false;
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
                this.playerValues.ReviveFrog();
                this.resetPlayerSpriteToFrog();
                this.setPlayerToCenterOfBottomLane();
                this.playerMovementManager.CanMove = true;
            }
        }

        private void resetPlayerSpriteToFrog()
        {
            this.player.Sprite.Visibility = Visibility.Collapsed;
            this.player.SetSprite(this.player.FrogSprite);
            this.player.Sprite.Visibility = Visibility.Visible;
        }

        private void gameOver()
        {
            this.timer.Stop();
            this.onGameOver();
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