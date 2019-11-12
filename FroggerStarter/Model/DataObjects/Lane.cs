using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using FroggerStarter.Model.GameObjects;

namespace FroggerStarter.Model.DataObjects
{
    /// <summary>
    ///     Represents a horizontal lane.
    /// </summary>
    public class Lane : IEnumerable<Vehicle>
    {
        #region Data members

        /// <summary>
        ///     The has water
        /// </summary>
        public readonly bool HasWater;

        private readonly int maxGameObjects;

        private Point location;
        private readonly double initialSpeed;
        private readonly double vehicleSpacing;
        private readonly GameObject.Direction direction;
        private readonly Vehicle.VehicleType vehicleType;
        private readonly WaterObject.WaterObjectType waterObjectType;
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
        ///     Initializes a new instance of the <see cref="Lane" /> class.
        /// </summary>
        /// <param name="vehicleType">Type of the vehicle.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="speed">The speed.</param>
        /// <param name="maxGameObjects">The maximum game objects.</param>
        public Lane(Vehicle.VehicleType vehicleType, GameObject.Direction direction, double speed, int maxGameObjects)
        {
            this.vehicleType = vehicleType;
            this.direction = direction;
            this.initialSpeed = speed;
            this.maxGameObjects = maxGameObjects;
            this.VehiclesInLane = new List<Vehicle>();
            this.vehicleSpacing = GameSettings.VehicleSpacing;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Lane" /> class.
        /// </summary>
        /// <param name="waterObjectType">Type of the water object.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="speed">The speed.</param>
        /// <param name="maxGameObjects">The maximum game objects.</param>
        /// <param name="hasWater">if set to <c>true</c> [has water].</param>
        public Lane(WaterObject.WaterObjectType waterObjectType, GameObject.Direction direction, double speed,
            int maxGameObjects, bool hasWater)
        {
            this.waterObjectType = waterObjectType;
            this.direction = direction;
            this.initialSpeed = speed;
            this.maxGameObjects = maxGameObjects;
            this.HasWater = hasWater;
            this.VehiclesInLane = new List<Vehicle>();
            this.vehicleSpacing = GameSettings.WaterObjectSpacing;
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
        ///     Occurs when [power up added].
        /// </summary>
        public event EventHandler<PowerUp> PowerUpAdded;

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
        ///     Removes all vehicles.
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
            var randomX = random.Next((int) (GameSettings.RoadWidth - powerUp.Width));
            powerUp.X = randomX;
            this.onPowerUpAdded(powerUp);
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
            if (this.HasWater)
            {
                var newVehicle = this.createWaterObject();
                this.placeObject(newVehicle);
                this.VehiclesInLane.Add(newVehicle);
                this.onVehicleAdded(newVehicle);
            }
            else
            {
                var newVehicle = this.createVehicle();
                this.placeObject(newVehicle);
                this.VehiclesInLane.Add(newVehicle);
                this.onVehicleAdded(newVehicle);
            }
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
                    return new Log(true, this.direction, this.initialSpeed);
                case WaterObject.WaterObjectType.Raft:
                    return new Raft(true, this.direction, this.initialSpeed);
                default:
                    return new Raft(true, this.direction, this.initialSpeed);
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
                if (currVehicle.X - this.vehicleSpacing < GameSettings.LeftEdgeOfRoad)
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
                if (currVehicle.X + currVehicle.Width + this.vehicleSpacing > GameSettings.RoadWidth)
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

        private void onPowerUpAdded(PowerUp powerUp)
        {
            this.PowerUpAdded?.Invoke(this, powerUp);
        }

        #endregion
    }
}