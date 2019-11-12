using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FroggerStarter.Model.DataObjects;
using FroggerStarter.Model.GameObjects;
using FroggerStarter.Utility;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages all aspects of the game play including moving the player,
    ///     the vehicles as well as lives and score.
    /// </summary>
    public class GameManager
    {
        #region Data members

        private readonly double backgroundHeight;
        private readonly double backgroundWidth;

        private Frog player;
        private PlayerValues playerValues;

        private Canvas gameCanvas;
        private DispatcherTimer timer;
        private Shoulder topShoulder;

        private RoadManager roadManager;
        private CollisionDetector collisionDetector;
        private LifeTimer lifeTimer;
        private PlayerMovementManager playerMovementManager;
        private AnimationManager animationManager;
        private IList<WaterCrossing> waterCrossings;

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

        /// <summary>
        ///     Gets the frog homes.
        /// </summary>
        /// <value>
        ///     The frog homes.
        /// </value>
        private IList<FrogHome> FrogHomes => this.topShoulder.FrogHomes;

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
        ///     Occurs when [game over reached].
        /// </summary>
        public event EventHandler GameOverReached;

        /// <summary>
        ///     Occurs when [lives updated].
        /// </summary>
        public event EventHandler<int> LivesUpdated;

        /// <summary>
        ///     Occurs when [score updated].
        /// </summary>
        public event EventHandler<int> ScoreUpdated;

        /// <summary>
        ///     Occurs when [life time updated].
        /// </summary>
        public event EventHandler<double> LifeTimerUpdated;

        private void setupGameTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.timerOnTick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, GameSettings.TimerMilliseconds);
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

            this.addFrogSprites();
            this.waterCrossings = new List<WaterCrossing>();
            this.instantiateHelperClasses();
            this.addNonPlayerSprites();

            this.roadManager.VehicleAdded += this.vehicleAdded;
            this.roadManager.VehicleRemoved += this.vehicleRemoved;
            this.roadManager.WaterObjectAdded += this.waterObjectAdded;
            this.roadManager.WaterObjectRemoved += this.waterObjectRemoved;
        }

        private void addNonPlayerSprites()
        {
            this.addVehiclesToCanvas();
            this.addFrogHomesToCanvas();
        }

        private void addFrogSprites()
        {
            this.createAndPlacePlayer();
            this.createFrogAnimationSprites();
        }

        private void instantiateHelperClasses()
        {
            this.playerValues = new PlayerValues();
            this.instantiateRoadManager();
            this.topShoulder = new Shoulder(this.player.Width);
            this.animationManager = new AnimationManager();
            this.collisionDetector = new CollisionDetector();
            this.lifeTimer = new LifeTimer();
            this.playerMovementManager = new PlayerMovementManager(this.player, this.FrogHomes);
        }

        private void instantiateRoadManager()
        {
            this.roadManager = new RoadManager();
            this.roadManager.WaterAdded += this.waterAdded;
            this.roadManager.GoToNextLevel(this.playerValues.CurrentLevel);
        }

        private void vehicleAdded(object sender, Vehicle vehicle)
        {
            this.gameCanvas.Children.Add(vehicle.Sprite);
        }

        private void vehicleRemoved(object sender, Vehicle vehicle)
        {
            this.gameCanvas.Children.Remove(vehicle.Sprite);
        }

        private void waterObjectAdded(object sender, WaterObject waterObject)
        {
            this.gameCanvas.Children.Add(waterObject.Sprite);
        }

        private void waterObjectRemoved(object sender, WaterObject waterObject)
        {
            this.gameCanvas.Children.Remove(waterObject.Sprite);
        }

        private void waterAdded(object sender, WaterCrossing waterCrossing)
        {
            this.gameCanvas.Children.Add(waterCrossing.Sprite);
            this.gameCanvas.Children.Move((uint) (this.gameCanvas.Children.Count -1), 0);
            this.waterCrossings.Add(waterCrossing);
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
            foreach (var currFrogHome in this.topShoulder.FrogHomes)
            {
                this.gameCanvas.Children.Add(currFrogHome.Sprite);
            }
        }

        private void removeFrogHomesFromCanvas()
        {
            foreach (var currFrogHome in this.topShoulder.FrogHomes)
            {
                this.gameCanvas.Children.Remove(currFrogHome.Sprite);
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
            this.roadManager.MoveObjects();
            this.checkToAddVehiclesToLanes();

            this.checkForPlayerCollision();

            this.checkIfFrogIsDoneDying();
            this.updateLifeTimer();
        }

        private void checkForPlayerCollision()
        {
            //this.checkPlayerCollisionWithPowerUps();
            this.checkPlayerCollisionWithVehicles();
            this.checkPlayerCollisionWithFrogHomes();
        }

        private void checkToAddVehiclesToLanes()
        {
            if (!this.playerValues.FrogDying)
            {
                this.roadManager.CheckToAddVehicleToLanes();
            }
        }

        private void updateLifeTimer()
        {
            this.lifeTimer.DecreaseTimeRemaining();
            this.onLifeTimerUpdated(this.lifeTimer.TimeRemaining);

            if (this.lifeTimer.TimeRemaining <= 0.0 && !this.playerValues.FrogDying)
            {
                SoundEffects.PlayTimeOutSound();
                this.playerLosesLife();
            }
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
            var enumerator = this.roadManager.AllVehicles.GetEnumerator();
            var vehicleCollision = false;
            var waterCollision = false;
            while (enumerator.MoveNext())
            {
                if (this.collisionDetector.IsCollisionBetween(enumerator.Current, this.player) &&
                    !this.playerValues.FrogDying)
                {
                    vehicleCollision = true;
                }
            }

            foreach (var currWaterCrossing in this.waterCrossings)
            {
                if (this.collisionDetector.IsCollisionBetween(currWaterCrossing, this.player) &&
                    !this.playerValues.FrogDying)
                {
                    waterCollision = true;
                }
            }

            if (vehicleCollision && !waterCollision)
            {
                SoundEffects.PlayDeathSound();
                this.playerLosesLife();
            }

            enumerator.Dispose();
        }

        private void checkPlayerCollisionWithFrogHomes()
        {
            var enumerator = this.topShoulder.FrogHomes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (this.collisionDetector.IsCollisionBetween(enumerator.Current, this.player))
                {
                    this.addFrogToFrogHome(enumerator.Current);
                }
            }

            enumerator.Dispose();
        }

        private void checkPlayerCollisionWithPowerUps()
        {
            foreach (var currPowerUp in this.roadManager.PowerUps)
            {
                if (this.collisionDetector.IsCollisionBetween(currPowerUp, this.player))
                {

                }
            }
        }

        private bool isPlayerOnWater()
        {
            foreach (var currWaterCrossing in this.waterCrossings)
            {
                if (this.collisionDetector.IsCollisionBetween(currWaterCrossing, this.player))
                {
                    return true;
                }
            }

            return false;
        }

        private void checkPlayerCollisionWithWaterObjects()
        {

        }

        private void addFrogToFrogHome(FrogHome frogHome)
        {
            SoundEffects.PlayHomeLandingSound();
            frogHome.AddFrog();
            this.gameCanvas.Children.Add(frogHome.Sprite);
            this.playerScores();
        }

        private bool allFrogHomesFilled()
        {
            var enumerator = this.topShoulder.FrogHomes.GetEnumerator();
            var homesFilled = 0;
            while (enumerator.MoveNext())
            {
                if (enumerator.Current != null && enumerator.Current.HasFrog)
                {
                    homesFilled++;
                }
            }

            enumerator.Dispose();
            return homesFilled == this.topShoulder.FrogHomes.Count;
        }

        private void playerLosesLife()
        {
            this.playerMovementManager.CanMove = false;
            this.playerValues.LoseALife();
            this.animateFrogDeath();
            this.onLivesUpdated(this.Lives);
            this.roadManager.ResetOneObjectPerLane();
        }

        private void animateFrogDeath()
        {
            this.animationManager.AnimateFrogDeath(this.player);
        }

        private void playerScores()
        {
            this.playerValues.IncreaseScore(this.lifeTimer.TimeRemaining);
            this.onScoreUpdated(this.Score);
            this.playerMovementManager.CanMove = false;
            this.playerValues.CheckForLevelCompleted(this.allFrogHomesFilled());
            this.resetFrogIfGameIsNotOver();
        }

        private void resetFrogIfGameIsNotOver()
        {
            if (this.playerValues.GameOver)
            {
                SoundEffects.PlayGameOverSound();
                this.gameOver();
            }
            else if (this.allFrogHomesFilled())
            {
                SoundEffects.PlayLevelCompleteSound();
                this.goToNextLevel();
            }
            else
            {
                this.playerValues.ReviveFrog();
                this.resetPlayerSpriteToFrog();
                this.setPlayerToCenterOfBottomLane();
                this.playerMovementManager.CanMove = true;
                this.lifeTimer.ResetTimeRemaining();
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
            this.onGameOverReached(EventArgs.Empty);
        }

        private void goToNextLevel()
        {
            this.removeFrogHomesFromCanvas();
            this.topShoulder.ClearHomes();
            this.addFrogHomesToCanvas();
            this.setPlayerToCenterOfBottomLane();
            this.roadManager.GoToNextLevel(this.playerValues.CurrentLevel);
            this.playerMovementManager.CanMove = true;
            this.lifeTimer.ResetTimeRemaining();
        }

        private void onScoreUpdated(int score)
        {
            this.ScoreUpdated?.Invoke(this, score);
        }

        private void onLivesUpdated(int lives)
        {
            this.LivesUpdated?.Invoke(this, lives);
        }

        private void onGameOverReached(EventArgs e)
        {
            this.GameOverReached?.Invoke(this, e);
        }

        private void onLifeTimerUpdated(double timeRemaining)
        {
            this.LifeTimerUpdated?.Invoke(this, timeRemaining);
        }

        #endregion
    }
}