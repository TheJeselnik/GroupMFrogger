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

        private readonly int maxVehicles;

        private Point location;
        private readonly double initialSpeed;
        private readonly Vehicle.Direction direction;
        private readonly Vehicle.VehicleType vehicleType;

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
        public Lane(Vehicle.VehicleType vehicleType, Vehicle.Direction direction, double speed, int maxVehicles)
        {
            this.vehicleType = vehicleType;
            this.direction = direction;
            this.initialSpeed = speed;
            this.maxVehicles = maxVehicles;
            this.VehiclesInLane = new List<Vehicle>();
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
        ///     Determines whether [has room for vehicles].
        ///     Precondition: none
        ///     Postcondition: True if VehiclesInLane.Count lessThan maxVehicles
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [has room for vehicles]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasRoomForVehicles()
        {
            return this.VehiclesInLane.Count < this.maxVehicles;
        }

        /// <summary>
        ///     Adds the vehicle.
        ///     Precondition: vehiclesInLane.Count lessThan maxVehicles
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
        /// Adds the power up.
        /// Precondition: None
        /// Postcondition: Powerup added to lane
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

        private void placeVehicle()
        {
            var newVehicle = this.createVehicle();
            var vehicleYOffset = calculateObjectYOffset(newVehicle);

            switch (this.direction)
            {
                case Vehicle.Direction.Left:
                    newVehicle.X = GameSettings.RoadWidth;
                    newVehicle.Y = this.Y - vehicleYOffset;
                    break;
                case Vehicle.Direction.Right:
                    newVehicle.X = GameSettings.LeftEdgeOfRoad - newVehicle.Width;
                    newVehicle.Y = this.Y - vehicleYOffset;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            this.VehiclesInLane.Add(newVehicle);
            this.onVehicleAdded(newVehicle);
        }

        private Vehicle createVehicle()
        {
            switch (this.vehicleType)
            {
                case Vehicle.VehicleType.Car:
                    return new Car(this.direction, this.initialSpeed);
                case Vehicle.VehicleType.SemiTruck:
                    return new SemiTruck(this.direction, this.initialSpeed);
                default:
                    return new Car(this.direction, this.initialSpeed);
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

        private void onVehicleAdded(Vehicle vehicle)
        {
            this.VehicleAdded?.Invoke(this, vehicle);
        }

        private void onVehicleRemoved(Vehicle vehicle)
        {
            this.VehicleRemoved?.Invoke(this, vehicle);
        }

        #endregion
    }
}