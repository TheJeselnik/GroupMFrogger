using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Represents a horizontal lane.
    /// </summary>
    public class Lane : IEnumerable<Vehicle>
    {
        #region Data members

        private readonly int maxGameObjects;

        private Point location;
        private readonly double initialSpeed;
        private readonly GameObject.Direction direction;
        private readonly Vehicle.VehicleType vehicleType;
        private readonly WaterObject.WaterObjectType waterObjectType;
        /// <summary>
        /// The has water
        /// </summary>
        public readonly bool HasWater;
        private bool waterPlaced;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the vehicles in lane.
        /// </summary>
        /// <value>
        ///     The vehicles in lane.
        /// </value>
        private IList<Vehicle> VehiclesInLane { get; }

        /// <summary>
        /// Gets the water objects in lane.
        /// </summary>
        /// <value>
        /// The water objects in lane.
        /// </value>
        private IList<WaterObject> WaterObjectsInLane { get; }

        /// <summary>
        ///     Gets or sets the y location of the lane.
        /// </summary>
        /// <value>
        ///     The y.
        /// </value>
        public double Y
        {
            get => this.location.Y;
            set => this.location.Y = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Lane" /> class.
        /// </summary>
        /// <param name="vehicleType">Type of the vehicle.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="speed">The speed.</param>
        /// <param name="maxGameObjects">The maximum game objects.</param>
        /// <param name="hasWater">if set to <c>true</c> [has water].</param>
        public Lane(Vehicle.VehicleType vehicleType, GameObject.Direction direction, double speed, int maxGameObjects,
            bool hasWater)
        {
            this.vehicleType = vehicleType;
            this.direction = direction;
            this.initialSpeed = speed;
            this.maxGameObjects = maxGameObjects;
            this.HasWater = hasWater;
            this.VehiclesInLane = new List<Vehicle>();
            this.WaterObjectsInLane = new List<WaterObject>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Lane"/> class.
        /// </summary>
        /// <param name="waterObjectType">Type of the water object.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="speed">The speed.</param>
        /// <param name="maxGameObjects">The maximum game objects.</param>
        /// <param name="hasWater">if set to <c>true</c> [has water].</param>
        public Lane(WaterObject.WaterObjectType waterObjectType, GameObject.Direction direction, double speed, int maxGameObjects,
            bool hasWater)
        {
            this.waterObjectType = waterObjectType;
            this.direction = direction;
            this.initialSpeed = speed;
            this.maxGameObjects = maxGameObjects;
            this.HasWater = hasWater;
            this.VehiclesInLane = new List<Vehicle>();
            this.WaterObjectsInLane = new List<WaterObject>();
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
            return this.VehiclesInLane.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        ///     Occurs when [vehicle added].
        /// </summary>
        public event EventHandler<Vehicle> VehicleAdded;

        /// <summary>
        ///     Occurs when [vehicle removed].
        /// </summary>
        public event EventHandler<Vehicle> VehicleRemoved;

        /// <summary>
        ///     Occurs when [water added].
        /// </summary>
        public event EventHandler<WaterCrossing> WaterAdded;

        /// <summary>
        /// Occurs when [water object added].
        /// </summary>
        public event EventHandler<WaterObject> WaterObjectAdded;

        /// <summary>
        /// Occurs when [water object removed].
        /// </summary>
        public event EventHandler<WaterObject> WaterObjectRemoved;

        /// <summary>
        ///     Determines whether [has room for vehicles].
        ///     Precondition: none
        ///     Postcondition: True if VehiclesInLane.Count lessThan maxGameObjects
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [has room for vehicles]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasRoomForVehicles()
        {
            return this.VehiclesInLane.Count < this.maxGameObjects;
        }

        /// <summary>
        /// Determines whether [has room for water objects].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has room for water objects]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasRoomForWaterObjects()
        {
            return this.WaterObjectsInLane.Count < this.maxGameObjects;
        }

        /// <summary>
        ///     Adds the vehicle.
        ///     Precondition: vehiclesInLane.Count lessThan maxGameObjects
        ///     Postcondition: New vehicle queued up
        /// </summary>
        public void AddVehicle()
        {
            if (this.vehiclesClearOfLeftEdge() && this.vehiclesClearOfRightEdge())
            {
                this.placeVehicle();
            }
        }

        /// <summary>
        /// Adds the water vehicle.
        ///     Precondition: waterObjectsInLane.Count lessThan maxGameObjects
        ///     Postcondition: New waterObject queued up
        /// </summary>
        public void AddWaterObject()
        {
            if (this.waterObjectsClearOfLeftEdge() && this.waterObjectsClearOfRightEdge())
            {
                this.placeWaterObject();
            }
        }

        /// <summary>
        ///     Removes the added vehicles.
        ///     Precondition: vehiclesInLane.Count greaterThan 1
        ///     Postcondition: Vehicle queued for deletion
        /// </summary>
        public void RemoveAddedVehicles()
        {
            const int firstItem = 1;
            foreach (var currVehicle in this.VehiclesInLane.TakeLast(this.VehiclesInLane.Count - firstItem))
            {
                this.VehiclesInLane.Remove(currVehicle);
                this.onVehicleRemoved(currVehicle);
            }
        }

        /// <summary>
        /// Removes the added water objects.
        ///     Precondition: waterObjectsInLane.Count greaterThan 1
        ///     Postcondition: WaterObject queued for deletion
        /// </summary>
        public void RemoveAddedWaterObjects()
        {
            const int firstItem = 1;
            foreach (var currWaterObject in this.WaterObjectsInLane.TakeLast(this.WaterObjectsInLane.Count - firstItem))
            {
                this.WaterObjectsInLane.Remove(currWaterObject);
                this.onWaterObjectRemoved(currWaterObject);
            }
        }

        /// <summary>
        /// Removes all vehicles.
        /// </summary>
        public void RemoveAllVehicles()
        {
            foreach (var currVehicle in this.VehiclesInLane)
            {
                this.onVehicleRemoved(currVehicle);
            }

            this.VehiclesInLane.Clear();
        }

        /// <summary>
        /// Removes all water objects.
        /// </summary>
        public void RemoveAllWaterObjects()
        {
            foreach (var currWaterObject in this.WaterObjectsInLane)
            {
                this.onWaterObjectRemoved(currWaterObject);
            }

            this.WaterObjectsInLane.Clear();
        }

        /// <summary>
        ///     Adds the power up.
        ///     Precondition: None
        ///     Postcondition: Powerup added to lane
        /// </summary>
        /// <param name="powerUp">The power up.</param>
        public void AddPowerUp(PowerUp powerUp)
        {
            var powerUpYOffset = calculateObjectYOffset(powerUp);
            powerUp.Y = this.Y - powerUpYOffset;
            var random = new Random();
            var randomX = random.Next((int) GameSettings.RoadWidth);
            powerUp.X = randomX;
        }

        /// <summary>
        ///     Adds the water.
        ///     Precondition: None
        ///     Postcondition: Water Crossing added to lane
        /// </summary>
        public void AddWater()
        {
            if (this.HasWater && !this.waterPlaced)
            {
                this.onWaterAdded(new WaterCrossing {Y = this.Y - GameSettings.WaterCrossingOffsetHeight});
            }

            this.waterPlaced = false;
        }

        private void placeVehicle()
        {
            var newVehicle = this.createVehicle();
            this.placeObject(newVehicle);
            this.VehiclesInLane.Add(newVehicle);
            this.onVehicleAdded(newVehicle);
        }

        private void placeWaterObject()
        {
            var waterObject = this.createWaterObject();
            this.placeObject(waterObject);
            this.WaterObjectsInLane.Add(waterObject);
            this.onWaterObjectAdded(waterObject);
        }

        private void placeObject(GameObject newObject)
        {
            var objectYOffset = calculateObjectYOffset(newObject);

            switch (this.direction)
            {
                case GameObject.Direction.Left:
                    newObject.X = GameSettings.RoadWidth;
                    newObject.Y = this.Y - objectYOffset;
                    break;
                case GameObject.Direction.Right:
                    newObject.X = GameSettings.LeftEdgeOfRoad - newObject.Width;
                    newObject.Y = this.Y - objectYOffset;
                    break;
                case GameObject.Direction.Up:
                    break;
                case GameObject.Direction.Down:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Vehicle createVehicle()
        {
            switch (this.vehicleType)
            {
                case Vehicle.VehicleType.Car:
                    return new Car(this.direction, this.initialSpeed);
                case Vehicle.VehicleType.SemiTruck:
                    return new SemiTruck(this.direction, this.initialSpeed);
                case Vehicle.VehicleType.OilSemiTruck:
                    return new OilSemiTruck(this.direction, this.initialSpeed);
                default:
                    return new Car(this.direction, this.initialSpeed);
            }
        }

        private WaterObject createWaterObject()
        {
            switch (this.waterObjectType)
            {
                case WaterObject.WaterObjectType.Log:
                    return new Log(true, this.initialSpeed);
                case WaterObject.WaterObjectType.Raft:
                    return new Raft(true, this.initialSpeed);
                default:
                    return new Raft(true, this.initialSpeed);
            }
        }

        private static double calculateObjectYOffset(GameObject newVehicle)
        {
            var emptyVerticalSpace = GameSettings.LaneHeight - newVehicle.Height;
            return emptyVerticalSpace / 2;
        }

        private bool vehiclesClearOfLeftEdge()
        {
            foreach (var currVehicle in this.VehiclesInLane)
            {
                if (currVehicle.X - GameSettings.VehicleSpacing < GameSettings.LeftEdgeOfRoad)
                {
                    return false;
                }
            }

            return true;
        }

        private bool vehiclesClearOfRightEdge()
        {
            foreach (var currVehicle in this.VehiclesInLane)
            {
                if (currVehicle.X + currVehicle.Width + GameSettings.VehicleSpacing > GameSettings.RoadWidth)
                {
                    return false;
                }
            }

            return true;
        }
        private bool waterObjectsClearOfLeftEdge()
        {
            foreach (var currWaterObject in this.WaterObjectsInLane)
            {
                if (currWaterObject.X - GameSettings.WaterObjectSpacing < GameSettings.LeftEdgeOfRoad)
                {
                    return false;
                }
            }

            return true;
        }

        private bool waterObjectsClearOfRightEdge()
        {
            foreach (var currWaterObject in this.WaterObjectsInLane)
            {
                if (currWaterObject.X + currWaterObject.Width + GameSettings.WaterObjectSpacing > GameSettings.RoadWidth)
                {
                    return false;
                }
            }

            return true;
        }

        private void onVehicleAdded(Vehicle vehicle)
        {
            this.VehicleAdded?.Invoke(this, vehicle);
        }

        private void onVehicleRemoved(Vehicle vehicle)
        {
            this.VehicleRemoved?.Invoke(this, vehicle);
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