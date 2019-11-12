using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FroggerStarter.Model.DataObjects;
using FroggerStarter.Model.GameObjects;
using FroggerStarter.Utility;
using FroggerStarter.View;

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
        private IList<PowerUp> powerUps;

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
            this.powerUps = new List<PowerUp>();
            this.instantiateHelperClasses();
            this.addNonPlayerSprites();
        }

        private void addNonPlayerSprites()
        {
            this.addVehiclesToCanvas();
            this.addFrogHomesToCanvas();
            this.addBushesToCanvas();
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
            this.playerMovementManager.PlayerMoved += this.playerMoved;
        }

        private void instantiateRoadManager()
        {
            this.roadManager = new RoadManager();
            this.roadManager.WaterAdded += this.waterAdded;
            this.roadManager.GoToNextLevel(this.playerValues.CurrentLevel);
            this.roadManager.VehicleAdded += this.vehicleAdded;
            this.roadManager.VehicleRemoved += this.vehicleRemoved;
            this.roadManager.PowerUpAdded += this.powerUpAdded;
        }

        private void vehicleAdded(object sender, Vehicle vehicle)
        {
            this.gameCanvas.Children.Add(vehicle.Sprite);
            this.moveSpriteToBottomOfCanvas(vehicle);
        }

        private void vehicleRemoved(object sender, Vehicle vehicle)
        {
            this.gameCanvas.Children.Remove(vehicle.Sprite);
        }

        private void waterAdded(object sender, WaterCrossing waterCrossing)
        {
            this.gameCanvas.Children.Add(waterCrossing.Sprite);
            this.gameCanvas.Children.Move((uint) (this.gameCanvas.Children.Count - 1), GameSettings.BottomCanvasIndex);
            this.waterCrossings.Add(waterCrossing);
        }

        private void powerUpAdded(object sender, PowerUp powerUp)
        {
            this.gameCanvas.Children.Add(powerUp.Sprite);
            this.powerUps.Add(powerUp);
        }

        private void playerMoved(object sender, GameObject.Direction direction)
        {
            this.animationManager.AnimateFrogJump(this.player, direction);
        }

        private void addVehiclesToCanvas()
        {
            foreach (var vehicle in this.roadManager.AllVehicles)
            {
                this.gameCanvas.Children.Add(vehicle.Sprite);
                this.moveSpriteToBottomOfCanvas(vehicle);
            }
        }

        private void moveSpriteToBottomOfCanvas(Vehicle vehicle)
        {
            if (vehicle is WaterObject)
            {
                this.gameCanvas.Children.Move((uint) (this.gameCanvas.Children.Count - 1), GameSettings.LowCanvasIndex);
            }
        }

        private void addFrogHomesToCanvas()
        {
            foreach (var currFrogHome in this.topShoulder.FrogHomes)
            {
                this.gameCanvas.Children.Add(currFrogHome.Sprite);
            }
        }

        private void addBushesToCanvas()
        {
            foreach (var currBush in this.topShoulder.Bushes)
            {
                this.gameCanvas.Children.Add(currBush.Sprite);
                this.gameCanvas.Children.Move((uint) (this.gameCanvas.Children.Count - 1), GameSettings.LowCanvasIndex);
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

            foreach (var currJumpSprite in this.player.JumpSprites)
            {
                currJumpSprite.Visibility = Visibility.Collapsed;
                this.gameCanvas.Children.Add(currJumpSprite);
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
            this.checkToAddRandomPowerUp();

            this.checkForPlayerCollision();

            this.checkIfFrogIsDoneDying();
            this.updateLifeTimer();
        }

        private void checkForPlayerCollision()
        {
            this.checkPlayerCollisionWithPowerUps();
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

        private void checkToAddRandomPowerUp()
        {
            if (!this.playerValues.FrogDying)
            {
                this.roadManager.CheckToAddRandomPowerUp();
            }
        }

        private void updateLifeTimer()
        {
            this.lifeTimer.DecreaseTimeRemaining();
            this.onLifeTimerUpdated(this.lifeTimer.TimeRemaining);

            if (this.lifeTimer.TimeRemaining <= 0.0 && !this.playerValues.FrogDying)
            {
                this.playerRunsOutOfTime();
            }
        }

        private void playerRunsOutOfTime()
        {
            SoundEffects.PlayTimeOutSound();
            this.playerLosesLife();
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
            while (enumerator.MoveNext())
            {
                if (this.playerCollidesWith(enumerator.Current))
                {
                    vehicleCollision = true;
                }

                if (enumerator.Current is WaterObject waterObject && vehicleCollision)
                {
                    waterObject.MoveLandedFrog(this.player);
                    if (this.playerMovementManager.PlayerAtLeftBoundary())
                    {
                        this.player.X = 0.0;
                    }

                    if (this.playerMovementManager.PlayerAtRightBoundary())
                    {
                        this.player.X = GameSettings.RoadWidth - this.player.Width;
                    }
                }

                if (vehicleCollision)
                {
                    break;
                }
            }

            if (vehicleCollision && !this.isPlayerOnWater())
            {
                this.playerGetsHit();
            }

            if (!vehicleCollision && this.isPlayerOnWater())
            {
                this.playerDrowns();
            }

            enumerator.Dispose();
        }

        private void playerDrowns()
        {
            SoundEffects.PlayWaterSplashSound();
            this.playerLosesLife();
        }

        private void playerGetsHit()
        {
            SoundEffects.PlayDeathSound();
            this.playerLosesLife();
        }

        private void checkPlayerCollisionWithFrogHomes()
        {
            var enumerator = this.topShoulder.FrogHomes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current != null)
                {
                    this.checkFrogHomeLanding(enumerator);
                }
            }

            if (this.player.Y <= GameSettings.TopEdgeOfLanes && !this.playerValues.FrogDying)
            {
                this.playerGetsHit();
            }

            enumerator.Dispose();
        }

        private void checkFrogHomeLanding(IEnumerator<FrogHome> enumerator)
        {
            if (enumerator.Current == null)
            {
                return;
            }

            var frogHomeWidth = enumerator.Current.Width;
            var cushion = frogHomeWidth * GameSettings.LandingCushionPercentage;
            if (this.collisionDetector.IsCollisionBetweenWithCushion(enumerator.Current, this.player, cushion) &&
                !enumerator.Current.HasFrog)
            {
                this.addFrogToFrogHome(enumerator.Current);
            }
            else if (this.collisionDetector.IsCollisionBetween(enumerator.Current, this.player) &&
                     !this.playerValues.FrogDying)
            {
                this.playerGetsHit();
            }
        }

        private void checkPlayerCollisionWithPowerUps()
        {
            PowerUp collectedPowerUp = null;
            foreach (var currPowerUp in this.powerUps)
            {
                if (this.collisionDetector.IsCollisionBetween(currPowerUp, this.player))
                {
                    collectedPowerUp = currPowerUp;
                }
            }

            this.collectPowerUp(collectedPowerUp);
        }

        private void collectPowerUp(PowerUp powerUp)
        {
            if (powerUp == null)
            {
                return;
            }

            this.gameCanvas.Children.Remove(powerUp.Sprite);
            this.powerUps.Remove(powerUp);
            switch (powerUp)
            {
                case TimePowerUp _:
                    this.lifeTimer.AddTime();
                    break;
                case ScorePowerUp _:
                    this.playerValues.AddBonusScore();
                    break;
            }

            this.onScoreUpdated(this.Score);
            SoundEffects.PlayPowerUpSound();
        }

        private bool isPlayerOnWater()
        {
            foreach (var currWaterCrossing in this.waterCrossings)
            {
                if (this.collisionDetector.IsCollisionBetween(currWaterCrossing, this.player) &&
                    !this.playerValues.FrogDying)
                {
                    return true;
                }
            }

            return false;
        }

        private bool playerCollidesWith(GameObject vehicle)
        {
            return vehicle != null &&
                   this.collisionDetector.IsCollisionBetween(vehicle, this.player) &&
                   !this.playerValues.FrogDying;
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
                this.gameOver();
            }
            else if (this.allFrogHomesFilled())
            {
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
            this.player.ResetSprite();
        }

        private void gameOver()
        {
            this.timer.Stop();
            this.onGameOverReached(EventArgs.Empty);
            SoundEffects.PlayGameOverSound();
            this.handleRetrievePlayerName();
        }

        private void goToNextLevel()
        {
            this.removeFrogHomesFromCanvas();
            this.removeWaterCrossings();
            this.topShoulder.ClearHomes();
            this.setPlayerToCenterOfBottomLane();
            this.roadManager.GoToNextLevel(this.playerValues.CurrentLevel);
            this.addFrogHomesToCanvas();
            this.playerMovementManager.CanMove = true;
            this.lifeTimer.ResetTimeRemaining();
            SoundEffects.PlayLevelCompleteSound();
        }

        private void removeWaterCrossings()
        {
            foreach (var currWaterCrossing in this.waterCrossings)
            {
                this.gameCanvas.Children.Remove(currWaterCrossing.Sprite);
            }

            this.waterCrossings.Clear();
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

        private async void handleRetrievePlayerName()
        {
            var level = this.playerValues.CurrentLevel;
            var score = this.playerValues.Score;

            if (this.playerValues.CurrentLevel > GameSettings.LevelsInGame)
            {
                level -= 1;
            }

            var dialog = new RetrievePlayerNameDialog(score, level);
            await dialog.ShowAsync();
        }

        #endregion
    }
}