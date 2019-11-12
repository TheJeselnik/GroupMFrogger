using System;
using System.Collections;
using System.Collections.Generic;
using FroggerStarter.Model.DataObjects;
using FroggerStarter.Model.GameObjects;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages lanes and vehicles
    /// </summary>
    public class RoadManager : IEnumerable<Vehicle>
    {
        #region Data members

        private int addVehicleTicks;
        private IList<Lane> currentLevelLanes;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets all vehicles.
        /// </summary>
        /// <value>
        ///     All vehicles.
        /// </value>
        public IList<Vehicle> AllVehicles { get; private set; }

        /// <summary>
        ///     The y coordinate of the top shoulder
        /// </summary>
        public double TopShoulderY { get; private set; }

        /// <summary>
        ///     Gets or sets the <see cref="Vehicle" /> at the specified index.
        /// </summary>
        /// <value>
        ///     The <see cref="Vehicle" />.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public Vehicle this[int index]
        {
            get => this.AllVehicles[index];
            set => this.AllVehicles.Insert(index, value);
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoadManager" /> class.
        /// </summary>
        public RoadManager()
        {
            this.currentLevelLanes = GameSettings.Levels[0];
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Vehicle> GetEnumerator()
        {
            return this.AllVehicles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        ///     Occurs when [gameObject added].
        /// </summary>
        public event EventHandler<Vehicle> VehicleAdded;

        /// <summary>
        ///     Occurs when [gameObject removed].
        /// </summary>
        public event EventHandler<Vehicle> VehicleRemoved;

        /// <summary>
        ///     Occurs when [gameObject removed].
        /// </summary>
        public event EventHandler<PowerUp> PowerUpAdded;

        /// <summary>
        ///     Occurs when [water added].
        /// </summary>
        public event EventHandler<WaterCrossing> WaterAdded;

        /// <summary>
        ///     Places the lanes.
        /// </summary>
        public void PlaceLanes()
        {
            this.AllVehicles = new List<Vehicle>();
            var currentY = GameSettings.RoadHeight - GameSettings.LaneHeight * 2 + GameSettings.RoadOffsetHeight;
            foreach (var currLane in this.currentLevelLanes)
            {
                currLane.Y = currentY;
                currentY -= GameSettings.LaneHeight;

                currLane.VehicleAdded += this.addNewVehicle;
                currLane.VehicleRemoved += this.removeVehicle;
                currLane.WaterAdded += this.addWater;
                currLane.PowerUpAdded += this.addPowerUp;

                currLane.AddWater();
            }

            this.addToLanes();
            this.getAllVehicles();
        }

        /// <summary>
        ///     Goes to next level.
        /// </summary>
        /// <param name="level">The level.</param>
        public void GoToNextLevel(int level)
        {
            this.addVehicleTicks = 0;
            foreach (var currLane in this.currentLevelLanes)
            {
                currLane.RemoveAllVehicles();
            }

            this.currentLevelLanes = GameSettings.Levels[level - 1];
            this.PlaceLanes();
        }

        private void addNewVehicle(object sender, Vehicle vehicle)
        {
            this.AllVehicles.Add(vehicle);
            this.onVehicleAdded(vehicle);
        }

        private void removeVehicle(object sender, Vehicle vehicle)
        {
            this.AllVehicles.Remove(vehicle);
            this.onVehicleRemoved(vehicle);
        }

        private void addWater(object sender, WaterCrossing waterCrossing)
        {
            this.onWaterAdded(waterCrossing);
        }

        private void addPowerUp(object sender, PowerUp powerUp)
        {
            this.onPowerUpAdded(powerUp);
        }

        /// <summary>
        ///     Moves the objects
        ///     Precondition: None
        ///     Postcondition: ObjectsInLane X == X@prev + Speed
        /// </summary>
        public void MoveObjects()
        {
            foreach (var currVehicle in this.AllVehicles)
            {
                resetObjectIfPastRightBoundary(currVehicle);
                resetObjectIfPastLeftBoundary(currVehicle);
                currVehicle.Move();
            }
        }

        private void addPowerUp()
        {
            var random = new Random();
            var index = random.Next(this.currentLevelLanes.Count - 1);
            var chosenLane = this.currentLevelLanes[index];

            if (chosenLane.HasWater)
            {
                chosenLane.AddPowerUp(new ScorePowerUp());
            }
            else
            {
                chosenLane.AddPowerUp(new TimePowerUp());
            }
        }

        /// <summary>
        ///     Adds a gameObject to lanes.
        ///     Precondition: currVehicleLane.VehiclesInLane.Count lessThan currVehicleLane.MaxVehicles
        ///     Postcondition: New gameObject queued up in currVehicleLane
        /// </summary>
        public void CheckToAddVehicleToLanes()
        {
            if (this.addVehicleTicks % GameSettings.TicksUntilSpawnObjects == 0)
            {
                this.addToLanes();
                this.addVehicleTicks = 0;
            }

            this.addVehicleTicks++;
        }

        /// <summary>
        ///     Checks to add random power up.
        ///     Precondition: Random Chance > 99.9%
        ///     Postcondition: TimePowerUp added to random lane
        /// </summary>
        public void CheckToAddRandomPowerUp()
        {
            var random = new Random();
            var chance = random.NextDouble();
            if (chance >= GameSettings.BonusTimePowerUpChance)
            {
                this.addPowerUp();
            }
        }

        private void addToLanes()
        {
            foreach (var currLane in this.currentLevelLanes)
            {
                if (currLane.HasRoomForVehicles())
                {
                    currLane.AddVehicle();
                }
            }
        }

        /// <summary>
        ///     Resets the objects to one.
        ///     Precondition: currLane.ObjectsInLane.Count greaterThan 1
        ///     Postcondition: Object in currLane queued for deletion
        /// </summary>
        public void ResetOneObjectPerLane()
        {
            foreach (var currLane in this.currentLevelLanes)
            {
                currLane.RemoveAddedVehicles();
            }
        }

        private static void resetObjectIfPastLeftBoundary(GameObject gameObject)
        {
            if (gameObject.X + gameObject.Width < GameSettings.LeftEdgeOfRoad)
            {
                gameObject.X = GameSettings.RoadWidth;
            }
        }

        private static void resetObjectIfPastRightBoundary(GameObject gameObject)
        {
            if (gameObject.X > GameSettings.RoadWidth)
            {
                gameObject.X = GameSettings.LeftEdgeOfRoad - gameObject.Width;
            }
        }

        private void getAllVehicles()
        {
            this.AllVehicles = new List<Vehicle>();
            foreach (var currLane in this.currentLevelLanes)
            {
                var enumerator = currLane.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    this.AllVehicles.Add(enumerator.Current);
                }

                enumerator.Dispose();
            }
        }

        private void onVehicleAdded(Vehicle vehicle)
        {
            this.VehicleAdded?.Invoke(this, vehicle);
        }

        private void onVehicleRemoved(Vehicle vehicle)
        {
            this.VehicleRemoved?.Invoke(this, vehicle);
        }

        private void onPowerUpAdded(PowerUp powerUp)
        {
            this.PowerUpAdded?.Invoke(this, powerUp);
        }

        private void onWaterAdded(WaterCrossing waterCrossing)
        {
            this.WaterAdded?.Invoke(this, waterCrossing);
        }

        #endregion
    }
}