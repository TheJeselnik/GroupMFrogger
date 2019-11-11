using System;
using System.Collections;
using System.Collections.Generic;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages lanes and vehicles
    /// </summary>
    public class RoadManager : IEnumerable<Vehicle>
    {
        #region Data members

        private int addObjectTicks;
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
        /// Gets the power ups.
        /// </summary>
        /// <value>
        /// The power ups.
        /// </value>
        public IList<PowerUp> PowerUps { get; private set; }

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

        /// <summary>
        /// Initializes a new instance of the <see cref="RoadManager"/> class.
        /// </summary>
        public RoadManager()
        {
            this.currentLevelLanes = GameSettings.Levels[0];
        }

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
        ///     Occurs when [waterObject added].
        /// </summary>
        public event EventHandler<WaterObject> WaterObjectAdded;

        /// <summary>
        ///     Occurs when [waterObject removed].
        /// </summary>
        public event EventHandler<WaterObject> WaterObjectRemoved;

        /// <summary>
        ///     Occurs when [gameObject removed].
        /// </summary>
        public event EventHandler<PowerUp> PowerUpAdded;

        /// <summary>
        ///     Occurs when [water added].
        /// </summary>
        public event EventHandler<WaterCrossing> WaterAdded;

        /// <summary>
        /// Places the lanes.
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
                currLane.WaterObjectAdded += this.addNewWaterObject;
                currLane.WaterObjectRemoved += this.removeWaterObject;

                currLane.AddWater();
            }

            this.addToLanes();
            this.getAllVehicles();
        }

        /// <summary>
        /// Goes to next level.
        /// </summary>
        /// <param name="level">The level.</param>
        public void GoToNextLevel(int level)
        {
            this.addObjectTicks = 0;
            foreach (var currLane in this.currentLevelLanes)
            {
                currLane.RemoveAllVehicles();
                currLane.RemoveAllWaterObjects();
            }

            this.currentLevelLanes = GameSettings.Levels[level - 1];
            this.PlaceLanes();
        }

        private void removeWaterObject(object sender, WaterObject waterObject)
        {
            this.onWaterObjectRemoved(waterObject);
        }

        private void addNewWaterObject(object sender, WaterObject waterObject)
        {
            this.onWaterObjectAdded(waterObject);
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

        /// <summary>
        /// Adds the power up to a random lane.
        ///     Precondition: None
        ///     Postcondition: RandomLane.AddPowerUp
        /// </summary>
        /// <param name="powerUp">The power up.</param>
        public void AddPowerUp(PowerUp powerUp)
        {
            var random = new Random();
            var index = random.Next(this.currentLevelLanes.Count - 1);
            this.currentLevelLanes[index].AddPowerUp(powerUp);
        }

        /// <summary>
        ///     Adds a gameObject to lanes.
        ///     Precondition: currVehicleLane.VehiclesInLane.Count lessThan currVehicleLane.MaxVehicles
        ///     Postcondition: New gameObject queued up in currVehicleLane
        /// </summary>
        public void CheckToAddVehicleToLanes()
        {
            if (this.addObjectTicks % GameSettings.TicksUntilSpawnObjects == 0)
            {
                this.addToLanes();
                this.addObjectTicks = 0;
            }

            this.addObjectTicks++;
        }

        private void addToLanes()
        {
            foreach (var currLane in this.currentLevelLanes)
            {
                if (currLane.HasRoomForVehicles() && !currLane.HasWater)
                {
                    currLane.AddVehicle();
                }

                if (currLane.HasRoomForWaterObjects() && currLane.HasWater)
                {
                    currLane.AddWaterObject();
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
                currLane.RemoveAddedWaterObjects();
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
            foreach (var currLane in GameSettings.FirstLevelLanes)
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

        //TODO potentially unnecessary
        private void onPowerUpAdded(PowerUp powerUp)
        {
            this.PowerUpAdded?.Invoke(this, powerUp);
        }

        private void onWaterAdded(WaterCrossing waterCrossing)
        {
            this.WaterAdded?.Invoke(this, waterCrossing);
        }

        private void onWaterObjectAdded(WaterObject waterObject)
        {
            this.WaterObjectAdded?.Invoke(this, waterObject);
        }

        private void onWaterObjectRemoved(WaterObject waterObject)
        {
            this.WaterObjectRemoved?.Invoke(this, waterObject);
        }

        #endregion
    }
}